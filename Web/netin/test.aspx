<%@ Page Language="C#" AutoEventWireup="false" CodeFile="test.aspx.cs" Inherits="Netin_test" %>

<!DOCTYPE html>
<html>
<head><title></title></head>
<body>
    <pre>
    M = moveto
    L = lineto
    H = horizontal lineto
    V = vertical lineto
    C = curveto
    S = smooth curveto
    Q = quadratic Belzier curve
    T = smooth quadratic Belzier curveto
    A = elliptical Arc
    Z = closepath
    </pre>
    <h3>基本</h3>
    <svg width="1200" height="600" version="1.1"
        xmlns="http://www.w3.org/2000/svg">

        <circle cx="100" cy="50" r="40" stroke="black"
            stroke-width="2" fill="red" />

        <polyline points="0,0 0,20 20,20 20,40 40,40 40,60"
            style="fill: white; stroke: red; stroke-width: 2" />

        <ellipse cx="300" cy="150" rx="200" ry="80"
            style="fill: rgb(200,100,50); stroke: rgb(0,0,100); stroke-width: 2;" />

        <rect width="300" height="100" rx="0" ry="0" x="500" y="50"
            style="fill: rgb(0,0,255); stroke-width: 1; stroke: rgb(0,0,0);" />


        <polygon points="220,100 300,210 170,250"
            style="fill: #cccccc; stroke: #000000; stroke-width: 1" />

        <path d="M250 150 L150 350 L350 350 Z" fill="#fff" stroke="#000" />


        <line x1="0" y1="0" x2="300" y2="300"
            style="stroke: rgb(99,99,99); stroke-width: 2" />

    </svg>

    <h3>线性渐变</h3>
    <svg width="100%" height="600" version="1.1"
        xmlns="http://www.w3.org/2000/svg">

        <defs>
            <linearGradient id="orange_red" x1="0%" y1="0%" x2="100%" y2="0%">
                <stop offset="0%" style="stop-color: rgb(255,255,0); stop-opacity: 1" />
                <stop offset="100%" style="stop-color: rgb(255,0,0); stop-opacity: 1" />
            </linearGradient>
        </defs>

        <ellipse cx="200" cy="190" rx="85" ry="55"
            style="fill: url(#orange_red)" />

    </svg>


    <h3>放射渐变</h3>
    <svg width="100%" height="600" version="1.1"
        xmlns="http://www.w3.org/2000/svg">

        <defs>
            <radialGradient id="grey_blue" cx="20%" cy="40%" r="50%"
                fx="50%" fy="50%">
                <stop offset="0%" style="stop-color: rgb(200,200,200); stop-opacity: 0" />
                <stop offset="100%" style="stop-color: rgb(0,0,255); stop-opacity: 1" />
            </radialGradient>
        </defs>

        <ellipse cx="230" cy="200" rx="110" ry="100"
            style="fill: url(#grey_blue)" />

    </svg>

    <h3>滤镜</h3>
    <svg width="100%" height="600" version="1.1"
        xmlns="http://www.w3.org/2000/svg">

        <defs>
            <filter id="Gaussian_Blur">
                <feGaussianBlur in="SourceGraphic" stdDeviation="20" />
            </filter>
        </defs>

        <ellipse cx="200" cy="150" rx="70" ry="40"
            style="fill: #ff0000; stroke: #000000; stroke-width: 2; filter: url(#Gaussian_Blur)" />

    </svg>

</body>
</html>
