# Ceasar Cipher
This is fully functional. Allowed characters are all ASCIIs from 32 to 126. 


# RSA
At the moment this only correctly encrypts seven characters at a time due to the modulus being such a small number. The Random class limits generation of numbers to only int datatype. This can be fixed by creating class that generates random BigIntegers, however it would be interesting to use a hardware random number generator.

Currently this generates primes of up to 10 digits long, which can create 64-bit RSA keys. Due to checking numbers longer than 10 digits for primality taking an excessive amount of time, I will have implement a probabilistic way to generate primes like Miller-Rabin. 


# AES
Not yet functional.
