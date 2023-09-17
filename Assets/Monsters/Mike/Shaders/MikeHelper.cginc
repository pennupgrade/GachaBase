#define PI 3.141592
#define TAU 6.2831
#define V float2(1., 0.)

float amod(float f, float m)
{
    float s = fmod(abs(f), m);
    return s + step(s, 0.) * (m - 2. * s);
}

float2 rot(float2 v, float o)
{
    return mul(float2x2(cos(o), sin(o), -sin(o), cos(o)), v);
}

float2 toPolar(float2 cart)
{
    return float2(length(cart), amod(atan2(cart.y, cart.x)+TAU, TAU));
}

float2 toCartesian(float2 polar)
{
    return polar.x * float2(cos(polar.y), sin(polar.y));
}

float2 perp(float2 v)
{
    return mul(float2x2(0., 1., -1., 0.), v);
}

float sdBox(float2 p, float2 fo, float2 dim)
{
    p = abs(float2(dot(p, fo), dot(p, perp(fo)))) - dim*.5;
    float s = sqrt(max(0., p.x*p.x*sign(p.x)) + max(0., p.y*p.y*sign(p.y)));
    return step(s, 0.) * max(p.x, p.y) + s;
}

float sdLine(float2 p, float2 sp, float2 ep, float thickness)
{
    p -= sp;
    float2 fo = normalize(ep - sp), up = perp(fo);
    p = float2(dot(p, fo), dot(p, up));
    
    return thickness > 0. ? abs(p.y) - thickness*.5 : p.y;
}

float choose(float2 vals, float choose)
{
    return vals.x + choose * (vals.y - vals.x);
}

float2 linearCircle(float t, float rMax, float2 spd, float seed)
{
    return toCartesian(float2(rMax * sin(t * spd.x + seed*100.), t*spd.y + seed*100.));
}

float sdSpikeball(float2 p, int sides, float2 rRange, float thickness, float o) //only 1 cell accuracy..
{

    //
    float2 polar = toPolar(p); polar.y -= o;
    float partSize = TAU / float(sides);
    float rTheta = amod(polar.y, partSize) - partSize*.5;
    float2 lp = toCartesian(float2(polar.x, rTheta));
    
    //
    float id = (amod(polar.y, partSize*2.) - amod(polar.y, partSize*1.)) / partSize;
    float r1 = choose(rRange.xy, id);
    float r2 = choose(rRange.yx, id);
    
    //
    float2 sp = toCartesian(float2(r1, -partSize*.5));
    float2 ep = toCartesian(float2(r2,  partSize*.5));
    
    //
    return sdLine(lp, sp, ep, thickness);
    
}

float highlightAllow = 1.;


float sbHighlight(float2 p, float2 start, float2 end, float2 highlightDim, float2 domainDim, float t, float extraRestrict)
{
    p -= start;
    float2 fo = normalize(end - start);
    p = float2(dot(p, fo) - t, dot(p, perp(fo)));
    
    p.x = amod(p.x, domainDim.x) - domainDim.x*.5;
    p.y = abs(p.y);
    
    highlightAllow = min(highlightAllow, step((domainDim.y+highlightDim.y)*.5+extraRestrict, p.y));
    
    p = abs(p - float2(0., domainDim.y*.5));
    
    return 1. - step(p.x, highlightDim.x*.5) * step(p.y, highlightDim.y*.5);
    
}

float sdAngledLines(float2 p, float startTheta, float thetaWidth)
{
    float angDist = -abs(amod(toPolar(p).y-startTheta, TAU) - PI) + PI;
    return (angDist - thetaWidth*.5) * length(p);
}

float sdMultiAngledLines(float2 p, float domainCutCount, float thetaWidth, float thetaOffset)
{
    float2 polar = toPolar(p);
    float rTheta = amod(polar.y + thetaOffset, TAU / domainCutCount) - .5 * TAU / domainCutCount;

    float angDist = -abs(amod(rTheta, TAU) - PI) + PI;
    return (angDist - thetaWidth*.5) * polar.x;
}

float opU(float d1, float d2) { return min(d1, d2); }
float opS(float d1, float d2) { return max(d1, -d2); }
float opI(float d1, float d2) { return max(d1, d2); }

float sb(float d)
{
    return step(d, 0.);
}

float oscillateEye(float t, float offset)
{
    return .02*pow(sin(t+offset), 15.) + .02 + .01;
} //just lerp between anger and happy states modulate time and smoothstep

float getEyeState(float t) //[0, 1]
{
    float repLen = 50.;
    
    float angerPercent = .4;
    float k = .04;
    
    t = abs(amod(t/repLen, 1.) - .5);
    
    return smoothstep((angerPercent+k)*.5, (angerPercent-k)*.5, t);
}

float2 getEyeHeights(float state)
{
    float2 nH = float2(.01, .08);
    float2 aH = float2(.05, .02);

    return nH + (aH - nH) * state;
}

float dEyes(float2 p) //generaliez for the outline, param for distorting p
{
    return 1.;
}

float2 ffloat2(float p) {return float2(p, p);}

float4 render(float2 uv)
{

    highlightAllow = 1.;

    float2 p = uv;
    p *= 1.0;

    float thickMult = 1.;//1.4;
    
    //
    float exists = 0.;
    
    //
    float d;
    
    p = rot(p, sin(_Time.y));
    float dHex = sdSpikeball(p, 8, ffloat2(.4), thickMult*.02, 0.);
    float dInner = sdSpikeball(p, 8, ffloat2(.3), thickMult*.02, 0.); dInner = opI(dInner, sdMultiAngledLines(p, 3., 1., 4.4*_Time.y));
    float dOuter = sdSpikeball(p, 8, ffloat2(.5), thickMult*.02, 0.); dOuter = opI(dOuter, sdAngledLines(p, (.7+.5)*_Time.y+10., 1.));
    float dAlpha = sdSpikeball(p, 8, ffloat2(.53), .0, 0.);
    
    //
    float et = 4.*_Time.y;
    int sCount = 5;
    float thick = .015;
    float r = .06+.012;
    
    float2 lEyePos = float2(-.1, .1) + 1.3*float2(0.8, 1.) * linearCircle(et, .014, float2(.5, .1), 5.7124125);
    float2 rEyePos = float2(.1, .1) + 1.3*float2(0.8, 1.) * linearCircle(et, .014, float2(.5, .1), 10.);
    
    float dLEye = sdSpikeball(p - lEyePos, sCount, ffloat2(r), thickMult*thick, et);
    float dREye = sdSpikeball(p - rEyePos, sCount, ffloat2(r), thickMult*thick, -et);
    
    float dLEyeZone = sdSpikeball(p - lEyePos, sCount, ffloat2(r+thickMult*thick*.5), 0., et);
    float dREyeZone = sdSpikeball(p - rEyePos, sCount, ffloat2(r+thickMult*thick*.5), .0, -et);
    
    float2 eyeHeight = getEyeHeights(getEyeState(et));
    
    float4 lEyeLine = lEyePos.xyxy + float4(-.1, eyeHeight.x, .1, eyeHeight.y);
    float4 rEyeLine = rEyePos.xyxy + float4(-.1, eyeHeight.y, .1, eyeHeight.x);
    
    float dLEyeLine = opI(sdLine(p, lEyeLine.xy, lEyeLine.zw, thickMult*thick), dLEyeZone);
    float dREyeLine = opI(sdLine(p, rEyeLine.xy, rEyeLine.zw, thickMult*thick), dREyeZone);
    
    float dLEyePartition = sdLine(p, lEyeLine.xy, lEyeLine.zw, 0.);
    float dREyePartition = sdLine(p, rEyeLine.xy, rEyeLine.zw, 0.);
    
    dLEyeZone = opS(dLEyeZone, dLEyePartition);
    dREyeZone = opS(dREyeZone, dREyePartition);
    
    dLEye = opU(opS(dLEye, dLEyePartition), dLEyeLine);
    dREye = opU(opS(dREye, dREyePartition), dREyeLine);
    
    float dForbiddenHighlight = opU(dLEyeZone, dREyeZone);

    //
    float dHighlightRegion = opS(sdSpikeball(p, 8, ffloat2(.28), 0., 0.), dForbiddenHighlight);

    float2 hhDim = thickMult*.94*1.4*1.4*float2(.04, .01);
    float2 hdDim = 1.2*1.4*float2(.09, .045);
    float xRestrict = .014*1.8;
    float t = .8*_Time.y;
    
    float dHighlight = sbHighlight(p, float2(-1., -.3), float2(1., .1), hhDim, hdDim, t, xRestrict);
    dHighlight = min(dHighlight, (1.-highlightAllow)+ sbHighlight(p, float2(-1., 1.5), float2(1., -1.), hhDim, hdDim, t, xRestrict));
    dHighlight = min(dHighlight, (1.-highlightAllow)+ sbHighlight(p, float2(-.7, -1.), float2(1.1, 2.), hhDim, hdDim, t, xRestrict));
    dHighlight = min(dHighlight, (1.-highlightAllow)+ sbHighlight(p, float2(-.7, .85), float2(.5, -.9), hhDim, hdDim, t, xRestrict));
    //dHighlight = min(dHighlight, (1.-highlightAllow)+ sbHighlight(p, float2(-.7, .2), float2(.7, -.3), hhDim, hdDim, t, xRestrict));
    //dHighlight = min(dHighlight, (1.-highlightAllow)+ sbHighlight(p, float2(-.76, -.7), float2(.8, .7), hhDim, hdDim, t, xRestrict));
    dHighlight = opI(dHighlight, dHighlightRegion);
    
    d = opU(dHex, dInner);
    d = opU(d, dOuter);
    d = opU(d, dHighlight);
    d = opU(d, dLEye);
    d = opU(d, dREye);
    
    exists += sb(d);
    
    //
    return float4(exists, exists, exists, sb(dAlpha));
}