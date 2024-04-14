# Ceasar Cipher
This is fully functional. Allowed characters are all ASCIIs from 32 to 126 (inclusive).


# RSA
This only encrypts seven characters at a time due to the modulus being such a small number. The Random class limits generation of numbers to only int datatype. This will be fixed by creating a method that generates random BigIntegers using Random. However, eventually a hardware random number generator will take the place of the random class.
Currently this generates primes of up to 10 digits long, which can create 64-bit RSA keys (much wow! such secure!). Due to checking numbers longer than 10 digits for primality taking an excessive amount of time, a probabilistic way to generate primes like Miller-Rabin will be implemented. 


# AES
This is fully functional. 128, 192, and 256 bit keys can be generated or a prior generated key can be used. Byte arrays or UTF-8 character strings can be encrypted.


# Morse Code
This is fully functional, though more will be added. Plain text can be converted to Morse Code and then played. The tone frequency and speed (in words per minute) can be selected. Eventually the sound quality will be improved and the option of Farnsworth Timing provided.