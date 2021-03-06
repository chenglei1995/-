function Q=PathWay(Q0,Q1,N)
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
T0=fkine(b123,Q0);
T1=fkine(b123,Q1);
Vx0=T0(1,4);
Vy0=T0(2,4);
Vz0=T0(3,4);
Vx1=T1(1,4);
Vy1=T1(2,4);
Vz1=T1(3,4);
Vn=[(Vx1-Vx0)/N,(Vy1-Vy0)/N,(Vz1-Vz0)/N];
Trans=[1,0,0,Vn(1);0,1,0,Vn(2);0,0,1,Vn(3);0,0,0,1];
Q(1,:)=Q0;
for i=2:N-1;
T0=Trans*T0;
q=ikine(b123,T0,Q(i-1,:));
Q(i,:)=q;
end
Q(N,:)=Q1;

