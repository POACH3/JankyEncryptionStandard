# Ceasar Cipher
## Encryption Process
Ceasar Cipher encrypts by shifting the plain text message by a certain number of places. A shift of 1 means any instance of A in the plain text becomes B, B becomes C, C becomes D,... Z becomes A, etc. This is a symmetric encryption, so the key (which is the shift value) is predetermined by the parties sending messages.
## Implementation
This implementation is fully functional. Allowed characters are all ASCIIs from 32 to 126 (inclusive).


# RSA
## Encryption Process
Rivest-Shamir-Adleman is an asymetric encryption method.
## Implementation
This only encrypts seven characters at a time due to the modulus being such a small number. The Random class limits generation of numbers to only int datatype. This will be fixed by creating a method that generates random BigIntegers using Random. However, eventually a hardware random number generator will take the place of the random class.
Currently this generates primes of up to 10 digits long, which can create 64-bit RSA keys (much wow! such secure!). Due to checking numbers longer than 10 digits for primality taking an excessive amount of time, a probabilistic way to generate primes like Miller-Rabin will be implemented. 


# AES
## Encryption Process
Advanced Encryption Standard (originally Rijndael) is a symmetric encryption. A 128, 192, or 256 key is randomly generated. This key is expanded into a set of "round keys" which are what is used to encrypt the data. Data subdivided into groups of 16 bytes which is organized into a 4x4 matrix. Each group of 16 bytes then undergoes multiple rounds of substitution (for a byte value in a predefined table), row shifting, column mixing, and is XORed with a round key.
## Implementation
Not yet functional. Still missing decrypt and column mixing functionality.
