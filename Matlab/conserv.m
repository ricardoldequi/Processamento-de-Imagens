function y1 = conserv(xng)
[r,c] = size(xng);
x1 = zeros(r+2,c+2);
x1(2:r+1,2:c+1,:) = xng(:,:);
[r,c] = size(x1);
y1 = x1;

for i = 2 : r-1
    for j = 2 : c-1
        nh = [x1(i-1,j-1) x1(i-1,j) x1(i-1,j+1) 
              x1(i,j-1)   x1(i,j)   x1(i,j+1) 
              x1(i+1,j-1) x1(i+1,j) x1(i+1,j+1)];
       
        cp = x1(i,j);
        mx = max(nh); 
        mn = min(nh);
        
        if (cp > mx) 
            cp = mx;
        else
            if (cp < mn)
                cp = mn;
            end
        end
        
        y1(i,j) = cp;
    end
end
return

