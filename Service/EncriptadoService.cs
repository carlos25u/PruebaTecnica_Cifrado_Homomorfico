using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PruebaTecnica_Cifrado_Homomorfico.Service
{
    internal class EncriptadoService
    {
        private Encryptor encryptor;
        private Decryptor decryptor;
        private KeyGenerator keyGenerator;
        private SecretKey secretKey;
        private PublicKey publicKey;
        private SEALContext context;
        private EncryptionParameters parms;
        private IntegerEncoder encoder;

        public EncriptadoService()
        {
            // Configurar los parámetros del cifrado homomórfico
            //define los parámetros del esquema de cifrado que se utilizará
            parms = new EncryptionParameters(SchemeType.BFV);
            // Establece el grado del polinomio modular.
            parms.PolyModulusDegree = 4096;
            //Define cómo los números son representados y manejados en el esquema.
            parms.CoeffModulus = CoeffModulus.BFVDefault(4096);
            // se utiliza para limitar el tamaño de los números que pueden ser cifrados.
            parms.PlainModulus = PlainModulus.Batching(4096, 20);

            context = new SEALContext(parms);

            // Generar claves pública y privada
            keyGenerator = new KeyGenerator(context);
            secretKey = keyGenerator.SecretKey;
            publicKey = keyGenerator.PublicKey;

            encryptor = new Encryptor(context, publicKey);
            decryptor = new Decryptor(context, secretKey);

            //se utiliza para crear un objeto de codificación 
            encoder = new IntegerEncoder(context);

        }

        public String Encriptar(String plaintext)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(plaintext);

                if (bytes.Length < 8)
                {
                    byte[] padding = new byte[8 - bytes.Length];
                    bytes = bytes.Concat(padding).ToArray();
                }

                long valorNumerico = BitConverter.ToInt64(bytes, 0);

                var textoplano = new Plaintext();
                encoder.Encode(valorNumerico, textoplano);

                var cipherText = new Ciphertext();
                encryptor.Encrypt(textoplano, cipherText);

                using (MemoryStream stream = new MemoryStream())
                {
                    cipherText.Save(stream);
                    string valorCipherText = Convert.ToBase64String(stream.ToArray());
                    return valorCipherText;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return plaintext;
            }
        }
        public String Desencriptar(string ciphertext)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(ciphertext);

                using (MemoryStream stream = new MemoryStream(cipherBytes))
                {
                    var cipherText = new Ciphertext();
                    cipherText.Load(context, stream);

                    var plainText = new Plaintext();

                    decryptor.Decrypt(cipherText, plainText);

                    long valorNumerico = encoder.DecodeInt64(plainText);
                    byte[] bytes = BitConverter.GetBytes(valorNumerico);

                    // Los datos desencriptados pueden tener bytes nulos (0x00) al final debido al relleno.
                    int nullTerminatorIndex = Array.IndexOf(bytes, (byte)0);
                    if (nullTerminatorIndex >= 0)
                    {
                        bytes = bytes.Take(nullTerminatorIndex).ToArray();
                    }

                    return Encoding.UTF8.GetString(bytes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al desencriptar texto: {e.ToString()}");
                return ciphertext; 
            }
        }
    }
}
