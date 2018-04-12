function Qr=Find(Q)
Q(1)=Q(1)-2*pi*floor(Q(1)/(2*pi));

if(Q(1)<-pi||Q(1)>=pi)
   Q(1)=Q(1)-2*pi;
end
if(Q(1)<-pi||Q(1)>=8/9*pi)
    error('½Ç¶È1³¬³ö·¶Î§');
end

Q(2)=Q(2)-2*pi*floor(Q(2)/(2*pi));
if(Q(2)<=-pi*180.75/180||Q(2)>179.25/180*pi)
   Q(2)=Q(2)-2*pi;
end
if(Q(2)<0||Q(2)>179.25/180*pi)
    error('½Ç¶È2³¬³ö·¶Î§');
end

Q(3)=Q(3)-2*pi*floor(Q(3)/(2*pi));
if(Q(3)<-pi/2||Q(3)>=3/2*pi)
   Q(3)=Q(3)-2*pi;
end
if(Q(3)<-pi/2||Q(3)>pi/2)
    error('½Ç¶È3³¬³ö·¶Î§');
end

Q(4)=Q(4)-2*pi*floor(Q(4)/(2*pi));
if(Q(4)<-156.62/180*pi||Q(4)>=203.96/180*pi)
   Q(4)=Q(4)-2*pi;
end
if(Q(4)<-156.62/180*pi||Q(4)>8/9*pi)
    error('½Ç¶È4³¬³ö·¶Î§');
end

Q(5)=Q(5)-2*pi*floor(Q(5)/(2*pi));
if(Q(5)<-115.02/180*pi||Q(5)>=244.98/180*pi)
   Q(5)=Q(5)-2*pi;
end
if(Q(5)<-115.02/180*pi||Q(5)>pi/2)
    error('½Ç¶È5³¬³ö·¶Î§');
end

Q(6)=Q(6)-2*pi*floor(Q(6)/(2*pi));
if(Q(6)<=-230.01/180*pi||Q(6)>129.99/180*pi)
   Q(6)=Q(6)-2*pi;
end
if(Q(6)<-170/180*pi||Q(6)>129.99/180*pi)
    error('½Ç¶È6³¬³ö·¶Î§');
end
Qr=Q;