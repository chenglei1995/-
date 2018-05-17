function TR= PosSolution(qz)
clear L
L{1} = link([ 0,0,pi/2,93,0], 'modified');
L{2} = link([ pi/2,80,pi/2,0,0], 'modified');
L{3} = link([0,260,0,0,0], 'modified');
L{4} = link([pi/2,80,0,300,0],'modified');
L{5} = link([-pi/2,0,0,0,0], 'modified');
L{6} = link([pi/2,0,0,0,0],'modified');
L{1}.qlim = [-181/180*pi,160/180*pi];
L{2}.qlim = [0*pi,179.27/180*pi];
L{3}.qlim = [-91/180*pi,90/180*pi];
L{4}.qlim = [-161.34/180*pi,160/180*pi];
L{5}.qlim = [-119.74/180*pi,90*pi];
L{6}.qlim = [-170/180*pi,127.5/180*pi];

b123 = robot(L, 'boshi123', 'Unimation', 'params of 8/95');
clear L
b123.name = 'boshi123';
b123.manuf = 'Unimation';
TR=fkine(b123,qz);


