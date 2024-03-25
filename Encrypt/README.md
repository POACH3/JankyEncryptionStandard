# Ceasar Cipher
Allowed characters currently are all ASCIIs from 32 to 122, but change this may change.

Could use some fine tuning, but this is essentially fully functional. 


# RSA
At the moment this only correctly encrypts seven characters at a time due to the modulus being such a small number. The Random class limits generation of numbers to only int datatype. This can be fixed by creating class that generates random BigIntegers, however it would be interesting to use a hardware random number generator.

I have no doubt that I am checking for primes in the most inefficient way possible...

Currently this generates primes of up to 10 digits long, which can create 64-bit RSA keys. Due to checking numbers longer than 10 digits for primality taking an excessive amount of time, I will have to reevaluate how I am checking or consider creating a table of primes to look up primality.