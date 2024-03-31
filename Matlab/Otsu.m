function T = Otsu(img)
h = imhist(img, 256);
p = h / sum(h);
omega = cumsum(p);
mu = cumsum(p .* (1:256));
muT = mu(end);
sigma_b = (muT .* omega - mu).^2 ./ (omega .* (1 - omega));
m = max(sigma_b);
T = mean(find(sigma_b == m));
end