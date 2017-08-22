makecertX64.exe ^
-n "CN=BodzioSamolot" ^
-r ^
-pe ^
-a sha512 ^
-len 4096 ^
-cy authority ^
-sv Certificate.pvk ^
Certificate.cer
 
pvk2pfxX64.exe ^
-pvk Certificate.pvk ^
-spc Certificate.cer ^
-pfx Certificate.pfx ^
-po Test123