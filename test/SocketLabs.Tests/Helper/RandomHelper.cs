using System;
using System.Collections.Generic;
using System.Linq;
using SocketLabs.InjectionApi.Message;

namespace SocketLabs.Tests.Helper
{
    internal class RandomHelper
    {

        public static readonly Random Randomizer = new Random();
        public static string RandomString()
        {
            return RandomString(RandomInt(0, 500));
        }

        public static string RandomString(int length, bool excludeNumbers = false)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (!excludeNumbers) chars += "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Randomizer.Next(s.Length)]).ToArray());
        }

        public static string RandomEmail(int nameCnt = 8, int domainCnt = 8, int suffixCnt = 3)
        {
            return $"{RandomString(nameCnt)}@{RandomString(nameCnt)}.{RandomString(suffixCnt, true)}";
        }

        public static List<IEmailAddress> RandomListOfRecipients(int count)
        {
            var recipients = new List<IEmailAddress>();
            for (int i = 0; i < count; i++)
            {
                recipients.Add(new EmailAddress(RandomEmail()));
            }
            return recipients;
        }

        public static List<IBulkRecipient> RandomListOfBulkRecipients(int count)
        {
            var recipients = new List<IBulkRecipient>();
            for (int i = 0; i < count; i++)
            {
                recipients.Add(new BulkRecipient(RandomEmail()));
            }
            return recipients;
        }

        public static int RandomInt()
        {
            return Randomizer.Next();
        }

        public static int RandomInt(int min, int max)
        {
            // make sure not to go over the largest int allowed
            if (max == int.MaxValue)
                max = int.MaxValue - 1;

            if (min >= max)
                throw new Exception("The min value must be less than the max value.");

            return Randomizer.Next(min, max + 1);
        }

        public static int RandomServerId()
        {
            return RandomInt(1, 21000);
        }

    }
}
