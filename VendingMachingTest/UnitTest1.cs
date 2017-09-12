using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineExercise;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VendingMachingTest
{
    [TestClass]
    public class UnitTest1
    {
        private static int MOCK_Account_Number = 73432;
       
        private static long MOCK_Card_Number1 = 50634576532;
        private static long MOCK_Card_Number2 = 90634576532;
        private const int NumberofItems = 25;
        private static int MOCK_Card_Number1_Pin = 6578;
        private static int MOCK_Card_Number2_Pin = 9786;
        private static readonly double Vend_Price = Global.pricePerVend;


        [TestMethod]
        public void CannotVendMoreThan25Items()
        {
            //Arrange
            int MOCK_Account_Balance = 200;
            IAccount account = new Account(MOCK_Account_Number);
            account.Balance = MOCK_Account_Balance;
        
            var card1 = new Card() { CardNumber = MOCK_Card_Number1, Account = account };
            var card2 = new Card() { CardNumber = MOCK_Card_Number2, Account = account };

            account.Cards = new List<ICard>() { card1, card2 };

            var vendingMachine = new VendingMachine(NumberofItems);
           
            var dictCardDetails = new Dictionary<long, int>();
            dictCardDetails.Add(MOCK_Card_Number1, MOCK_Card_Number1_Pin);
            dictCardDetails.Add(MOCK_Card_Number2, MOCK_Card_Number2_Pin);
            vendingMachine.CardDetails = dictCardDetails;  //Register Cards

            //Act
            try
            {
                for (int i = 0; i < 26; i++)
                {
                    var actual = vendingMachine.Vend(card1, MOCK_Card_Number1_Pin);
                }
                Assert.Fail();
            }
            catch (Exception ex)
            {
                  Assert.AreEqual(Global.VendEmpty, ex.Message);
            }
      


        }

        [TestMethod]
        public void CannotVendWhenBalanceIsLessThan50p()
        {
            //Arrange
            double MOCK_Account_Balance = 0.40;
            IAccount account = new Account(MOCK_Account_Number);
            account.Balance = MOCK_Account_Balance;

            var card1 = new Card() { CardNumber = MOCK_Card_Number1, Account = account };
            var card2 = new Card() { CardNumber = MOCK_Card_Number2, Account = account };

            account.Cards = new List<ICard>() { card1, card2 };

            var vendingMachine = new VendingMachine(NumberofItems);
            
            var dictCardDetails = new Dictionary<long, int>();
            dictCardDetails.Add(MOCK_Card_Number1, MOCK_Card_Number1_Pin);
            dictCardDetails.Add(MOCK_Card_Number2, MOCK_Card_Number2_Pin);
            vendingMachine.CardDetails = dictCardDetails; //Register Cards

            //Act
            try
            {
                var actual = vendingMachine.Vend(card1, MOCK_Card_Number1_Pin);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual(Global.NotEnoughBalance, ex.Message);
            }



        }


        [TestMethod]
        public void CannotVendWhenPINIsWrong()
        {
            //Arrange
            double MOCK_Account_Balance = 10;
            IAccount account = new Account(MOCK_Account_Number);
            account.Balance = MOCK_Account_Balance;

            var card1 = new Card() { CardNumber = MOCK_Card_Number1, Account = account };
            var card2 = new Card() { CardNumber = MOCK_Card_Number2, Account = account };

            account.Cards = new List<ICard>() { card1, card2 };

            var vendingMachine = new VendingMachine(NumberofItems);
            
            var dictCardDetails = new Dictionary<long, int>();
            dictCardDetails.Add(MOCK_Card_Number1, MOCK_Card_Number1_Pin);
            dictCardDetails.Add(MOCK_Card_Number2, MOCK_Card_Number2_Pin);
            vendingMachine.CardDetails = dictCardDetails; //Register Cards

            //Act
            try
            {
                var actual = vendingMachine.Vend(card1, MOCK_Card_Number1_Pin+1); //Send a wrong PIN
                Assert.Fail();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual(Global.IncorrectPIN, ex.Message);
            }



        }

        [TestMethod]
        public void CannotVendWhenCardNotregistered()
        {
            //Arrange
            double MOCK_Account_Balance = 10;
            IAccount account = new Account(MOCK_Account_Number);
            account.Balance = MOCK_Account_Balance;

            var card1 = new Card() { CardNumber = MOCK_Card_Number1, Account = account };
            var card2 = new Card() { CardNumber = MOCK_Card_Number2, Account = account };

            account.Cards = new List<ICard>() { card1, card2 };

            var vendingMachine = new VendingMachine(NumberofItems);

            

            //Act
            try
            {
                var actual = vendingMachine.Vend(card1, MOCK_Card_Number1_Pin); //Send a unregistred Card
                Assert.Fail();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.AreEqual(Global.UnregisteredCard, ex.Message);
            }



        }

        [TestMethod]
        public void CashCardBalanceUpdated()
        {
            //Arrange
            double MOCK_Account_Balance = 10;
            IAccount account = new Account(MOCK_Account_Number);// FakeAccount();
            account.Balance = MOCK_Account_Balance;

            var card1 = new Card() { CardNumber = MOCK_Card_Number1, Account = account };
            var card2 = new Card() { CardNumber = MOCK_Card_Number2, Account = account };

            account.Cards = new List<ICard>() { card1, card2 };

            var vendingMachine = new VendingMachine(NumberofItems);
            
            var dictCardDetails = new Dictionary<long, int>(); 
            dictCardDetails.Add(MOCK_Card_Number1, MOCK_Card_Number1_Pin);
            dictCardDetails.Add(MOCK_Card_Number2, MOCK_Card_Number2_Pin);
            vendingMachine.CardDetails = dictCardDetails; //Register Cards

            //Act
            var actual = vendingMachine.Vend(card1, MOCK_Card_Number1_Pin); 
               
           //Assert
            Assert.AreEqual(true, actual);
            double expectedBalance = MOCK_Account_Balance - Vend_Price; //After one vend
            double actualBalance = account.Balance;
            Assert.AreEqual(expectedBalance, actualBalance);

            //Act
            var actual2 = vendingMachine.Vend(card2, MOCK_Card_Number2_Pin);

            //Assert
            Assert.AreEqual(true, actual2);
            double expectedBalance2 = expectedBalance - Vend_Price; //After second vend
            double actualBalance2 = account.Balance;
            Assert.AreEqual(expectedBalance2, actualBalance2);





        }


        [TestMethod]
        public void CanAccessAccountConcurrently()
        {
            //Arrange
            double MOCK_Account_Balance = 10;
            IAccount account = new Account(MOCK_Account_Number);
            account.Balance = MOCK_Account_Balance;

            var card1 = new Card() { CardNumber = MOCK_Card_Number1, Account = account };
            var card2 = new Card() { CardNumber = MOCK_Card_Number2, Account = account };

            account.Cards = new List<ICard>() { card1, card2 };

            var vendingMachine = new VendingMachine(NumberofItems);
            
            var dictCardDetails = new Dictionary<long, int>();
            dictCardDetails.Add(MOCK_Card_Number1, MOCK_Card_Number1_Pin);
            dictCardDetails.Add(MOCK_Card_Number2, MOCK_Card_Number2_Pin);
            vendingMachine.CardDetails = dictCardDetails;   //Register Cards

            //Another hypothetical Vending machine to test Concurrency 
            var vendingMachine2 = new VendingMachine(NumberofItems);
            vendingMachine2.CardDetails = dictCardDetails;   //Register Cards

            //Act

            var actual1 = vendingMachine.Vend(card1, MOCK_Card_Number1_Pin); //First Vend from Card 1 On this Vending Machine
            Task<bool> actual2= Task.Run(() => vendingMachine.Vend(card2, MOCK_Card_Number2_Pin)); // Simultaneous Vend from Card 2 on another Vending Machine

            Assert.AreEqual(true, actual1);
            Assert.AreEqual(true, actual2.Result);
            double expectedBalance = MOCK_Account_Balance - 2*Vend_Price; //After two vends
            double actualBalance = account.Balance;
            Assert.AreEqual(expectedBalance, actualBalance);





        }

        [TestMethod]
        public void CanAccessAccountConcurrently_WithMoreSimultaneousVends()
        {
            //Arrange
            double MOCK_Account_Balance = 10;
            IAccount account = new Account(MOCK_Account_Number);// FakeAccount();
            account.Balance = MOCK_Account_Balance;

            var card1 = new Card() { CardNumber = MOCK_Card_Number1, Account = account };
            var card2 = new Card() { CardNumber = MOCK_Card_Number2, Account = account };

            account.Cards = new List<ICard>() { card1, card2 };

            var vendingMachine = new VendingMachine(NumberofItems);

            var dictCardDetails = new Dictionary<long, int>();
            dictCardDetails.Add(MOCK_Card_Number1, MOCK_Card_Number1_Pin);
            dictCardDetails.Add(MOCK_Card_Number2, MOCK_Card_Number2_Pin);
            vendingMachine.CardDetails = dictCardDetails;   //Register Cards

            //Another hypothetical Vending machine to test Concurrency 
            var vendingMachine2 = new VendingMachine(NumberofItems);
            vendingMachine2.CardDetails = dictCardDetails;   //Register Cards

            //Act

            var actual1 = vendingMachine.Vend(card1, MOCK_Card_Number1_Pin); //vend three times
            vendingMachine.Vend(card1, MOCK_Card_Number1_Pin);
            vendingMachine.Vend(card1, MOCK_Card_Number1_Pin);

            var t1=Task.Run(() => vendingMachine.Vend(card2, MOCK_Card_Number2_Pin)); // vend three times concurrently
            var t2 = Task.Run(() => vendingMachine.Vend(card2, MOCK_Card_Number2_Pin));
            var t3 = Task.Run(() => vendingMachine.Vend(card2, MOCK_Card_Number2_Pin));

            t1.Wait();
            t2.Wait();
            t3.Wait();
            double expectedBalance = MOCK_Account_Balance - 6 * Vend_Price; //After total 6 vends
            double actualBalance = account.Balance;
            Assert.AreEqual(expectedBalance, actualBalance);





        }

    }
}
