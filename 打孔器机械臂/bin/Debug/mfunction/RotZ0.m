function [Q,QD,QDD]=RotZ0(Q0,ANGLE,N,TIME)
clear L
L{1} = link([ 0,0,pi/2,93,0], 'modified');
L{2} = link([ pi/2,80,pi/2,0,0], 'modified');
L{3} = link([0,260,0,0,0], 'modified');
L{4} = link([pi/2,80,0,300,0],'modified');
L{5} = link([-pi/2,0,0,0,0], 'modified');
L{6} = link([pi/2,0,0,0,0],'modified');
L{1}.qlim = [-181/180*pi,160/180*pi];
L{2}.qlim = [0*pi,180.24/180*pi];
L{3}.qlim = [-91/180*pi,90/180*pi];
L{4}.qlim = [-157.62/180*pi,160/180*pi];
L{5}.qlim = [-121/180*pi,90*pi];
L{6}.qlim = [-170/180*pi,130.99/180*pi];
b123 = robot(L, 'boshi123', 'Unimation', 'params of 8/95');
clear L
b123.name = 'boshi123';
b123.manuf = 'Unimation';
T=fkine(b123,Q0);
R=T;
px=T(1,4);
py=T(2,4);
pz=T(3,4);
R(1,4)=0;
R(2,4)=0;
R(3,4)=0;
Trans=[1,0,0,px;0,1,0,py;0,0,1,pz;0,0,0,1];
tran=rotz(ANGLE);
T=tran*R;
T1=Trans*T;
Qr=ikine(b123,T1,Q0);
Qr=Find(Qr);
Time=linspace(0,TIME,N);
[Q,QD,QDD]=jtraj(Q0,Qr,Time);

