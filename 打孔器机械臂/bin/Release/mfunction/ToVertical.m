function [Q,QD,QDD]=ToVertical(Q0,N,TIME)
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
b123 = robot(L, 'boshi123', 'Unimation', 'params of 8/95');
clear L
b123.name = 'boshi123';
b123.manuf = 'Unimation';
T10=rotz(Q0(1))';
T=fkine(b123,Q0);
T16=T10*T;
a=T16(1,3);
b=T16(2,3);
c=T16(3,3);
vz=[a,b,c];
vx=[0,0,-1];
R16=T16;
px=T16(1,4);
py=T16(2,4);
pz=T16(3,4);
R16(1,4)=0;
R16(2,4)=0;
R16(3,4)=0;

Trans=[1,0,0,px;0,1,0,py;0,0,1,pz;0,0,0,1];
thetazx=atan2(sqrt(vz(1)^2+vz(2)^2),-vz(3));
vn=cross(vz,vx);
vn=vn/norm(vn)*thetazx;
thetaY=vn(2);
thetaX=vn(1);
thetaZ=vn(3);
tran=rotz(thetaZ)*roty(thetaY)*rotx(thetaX);
T16=tran*R16;
T16=Trans*T16;
T=T10'*T16;
Qr=ikine(b123,T,Q0);
Qr=Find(Qr);
Time=linspace(0,TIME,N);
[Q,QD,QDD]=jtraj(Q0,Qr,Time);


