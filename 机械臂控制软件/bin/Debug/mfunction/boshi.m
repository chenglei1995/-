function TR= PosSolution(qz)
clear L
L{1} = link([ -pi/2,0.16,0,0.19,0], 'standard');
L{2} = link([ 0,0.35,0,0,0], 'standard');
L{3} = link([-pi/2,0.13,0,0,0], 'standard');
L{4} = link([pi/2,0,0,0.44,0], 'standard');
L{5} = link([-pi/2,0,0,0,0], 'standard');
L{6} = link([0,0,0,0.24,0], 'standard');



b123 = robot(L, 'boshi123', 'Unimation', 'params of 8/95');
clear L
b123.name = 'boshi123';
b123.manuf = 'Unimation';
TR=fkine(b123,qz);


