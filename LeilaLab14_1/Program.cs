using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace LeilaLab14_1 {
    class Program {
        static Random rnd = new Random();

        static BigInteger gcd(BigInteger a, BigInteger b) {
            if (b == 0)
                return a;

            return gcd(b, a % b);
        }

        static BigInteger euklid(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y) {
            if (a == 0) {
                x = 0;
                y = 1;

                return b;
            }

            BigInteger x1, y1;

            BigInteger d = euklid(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;

            return d;
        }

        static BigInteger modPow(BigInteger a, BigInteger n, BigInteger mod) {
            if (n == 0)
                return 1;

            if (n % 2 == 1)
                return (modPow(a, n - 1, mod) * a) % mod;

            BigInteger b = modPow(a, n / 2, mod);
            return (b * b) % mod;
        }

        static bool prime(BigInteger n) {
            List<BigInteger> primes = new List<BigInteger>();
            primes.Add(2);

            for (BigInteger x = 3; x < n; x++) {
                bool isPrime = true;

                for (int i = 0; i < primes.Count && isPrime; i++)
                    if (x % primes[i] == 0)
                        isPrime = false;

                if (isPrime)
                    primes.Add(x);
            }

            return primes[primes.Count - 1] == n;
        }

        static bool ferma(BigInteger x) {
            if (x == 2)
                return true;
            
            for (int i = 0; i < 100; i++) {
                BigInteger a = (rnd.Next() % (x - 2)) + 2;
                if (gcd(a, x) != 1)
                    return false;

                if (modPow(a, x - 1, x) != 1)
                    return false;
            }
            return true;
        }

        static BigInteger secretExponent(BigInteger p, BigInteger q, BigInteger e) {
            BigInteger fi = (p - 1) * (q - 1); // euler function(n) = (p - 1)(q - 1)

            BigInteger d, y;
            euklid(e, fi, out d, out y);

            //if (d < 0)
            //    d += fi;

            return d;
        }

        static BigInteger encrypt(BigInteger m, BigInteger e, BigInteger n) {
            return modPow(m, e, n);
        }

        static BigInteger decrypt(BigInteger c, BigInteger d, BigInteger n) {
            return modPow(c, d, n);
        }

        // p, q - prime numbers, e - opened exponent
        static void RSA(BigInteger p, BigInteger q, BigInteger e) {
            BigInteger n = p * q; // modulus 
            BigInteger d = secretExponent(p, q, e);

            Console.WriteLine("p: {0}, q: {1}, n (p*q): {2}, e: {3}, d = {4}", p, q, n, e, d);

            Console.Write("Enter value to encrypt: ");
            BigInteger m = BigInteger.Parse(Console.ReadLine());

            BigInteger c = encrypt(m, e, n);

            Console.WriteLine("Encrypted: {0}", c);
            Console.WriteLine("Decrypted: {0}", decrypt(c, d, n));
        }

        static void Main(string[] args) {
            RSA(99971, 98929, 17);
            Console.ReadKey();
            
        }
    }
}
