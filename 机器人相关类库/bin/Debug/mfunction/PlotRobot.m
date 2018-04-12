function  PlotRobot(q)
clear L
L{1} = link([ 0,0,pi/2,93,0], 'modified');
L{2} = link([ pi/2,80,pi/2,0,0], 'modified');
L{3} = link([0,260,0,0,0], 'modified');
L{4} = link([pi/2,80,0,300,0],'modified');
L{5} = link([-pi/2,0,0,0,0], 'modified');
L{6} = link([pi/2,0,0,0,0],'modified');
L{1}.qlim = [0 1.8*pi];
L{2}.qlim = [0 pi];
L{3}.qlim = [-pi/2 pi/2];
L{4}.qlim = [-4/9*pi 4/9*pi];
L{5}.qlim = [-1/3*pi pi/2];
L{6}.qlim = [0 1.8*pi];

b123 = robot(L,' ', 'Unimation', 'params of 8/95');
clear L
%figure('NumberTitle', 'off', 'Name', '»úÐµ±Û·ÂÕæÍ¼');
plot(b123,q);
hold on;