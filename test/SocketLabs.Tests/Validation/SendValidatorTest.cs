using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Core;
using SocketLabs.InjectionApi.Message;
using SocketLabs.Tests.Helper;

namespace SocketLabs.Tests.Validation
{
    [TestClass]
    public class SendValidatorTest
    {
        #region ValidateIMessageBase

        [TestMethod]
        public void ValidateIMessageBase_ReturnsMessageValidationEmptySubject_WhenSubjectIsEmpty()
        {
            //Arrange
            var message = new BasicMessage { Subject = string.Empty };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateIMessageBase(message);

            //Assert
            Assert.AreEqual(SendResult.MessageValidationEmptySubject, actual);
        }

        [TestMethod]
        public void ValidateIMessageBase_ReturnsEmailAddressValidationMissingFrom_WhenFromRecipientIsNull()
        {
            //Arrange
            var message = new BasicMessage
            {
                Subject = RandomHelper.RandomString(),
                From = null
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateIMessageBase(message);

            //Assert
            Assert.AreEqual(SendResult.EmailAddressValidationMissingFrom, actual);
        }

        [TestMethod]
        public void ValidateIMessageBase_ReturnsEmailAddressValidationMissingFrom_WhenFromRecipientIsEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                Subject = RandomHelper.RandomString(),
                From = new EmailAddress(string.Empty)
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateIMessageBase(message);

            //Assert
            Assert.AreEqual(SendResult.EmailAddressValidationMissingFrom, actual);
        }

        [TestMethod]
        public void ValidateIMessageBase_ReturnsEmailAddressValidationInvalidFrom_WhenFromRecipientIsInvalid()
        {
            //Arrange
            var message = new BasicMessage
            {
                Subject = RandomHelper.RandomString(),
                From = new EmailAddress("$$##%%")
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateIMessageBase(message);

            //Assert
            Assert.AreEqual(SendResult.EmailAddressValidationInvalidFrom, actual);
        }

        [TestMethod]
        public void ValidateIMessageBase_ReturnsMessageValidationEmptyMessage_WhenSubjectAndFromRecipientAndAllBodyFieldsAreEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                Subject = RandomHelper.RandomString(),
                From = new EmailAddress(RandomHelper.RandomEmail())
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateIMessageBase(message);

            //Assert
            Assert.AreEqual(SendResult.MessageValidationEmptyMessage, actual);
        }

        [TestMethod]
        public void ValidateIMessageBase_ReturnsMessageValidationInvalidCustomHeaders_WhenCustomHeadersAreInvalid()
        {
            //Arrange
            var message = new BasicMessage
            {
                Subject = RandomHelper.RandomString(),
                From = new EmailAddress(RandomHelper.RandomEmail()),
                HtmlBody = RandomHelper.RandomString(),
                CustomHeaders = new List<ICustomHeader>()
                {
                    new CustomHeader(string.Empty, string.Empty)
                }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateIMessageBase(message);

            //Assert
            Assert.AreEqual(SendResult.MessageValidationInvalidCustomHeaders, actual);
        }

        [TestMethod]
        public void ValidateIMessageBase_ReturnsSuccess_WhenSubjectAndFromRecipientAndHtmlBodyIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                Subject = RandomHelper.RandomString(),
                From = new EmailAddress(RandomHelper.RandomEmail()),
                HtmlBody = RandomHelper.RandomString()
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateIMessageBase(message);

            //Assert
            Assert.AreEqual(SendResult.Success, actual);
        }

        [TestMethod]
        public void ValidateIMessageBase_ReturnsSuccess_WhenSubjectAndFromRecipientAndPlainTextBodyIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                Subject = RandomHelper.RandomString(),
                From = new EmailAddress(RandomHelper.RandomEmail()),
                PlainTextBody = RandomHelper.RandomString()
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateIMessageBase(message);

            //Assert
            Assert.AreEqual(SendResult.Success, actual);
        }

        [TestMethod]
        public void ValidateIMessageBase_ReturnsSuccess_WhenSubjectAndFromRecipientAndApiTemplateIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                Subject = RandomHelper.RandomString(),
                From = new EmailAddress(RandomHelper.RandomEmail()),
                ApiTemplate = RandomHelper.RandomInt()
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateIMessageBase(message);

            //Assert
            Assert.AreEqual(SendResult.Success, actual);
        }

        #endregion


        #region HasMessageBody

        [TestMethod]
        public void HasMessageBody_ReturnsFalse_WhenHtmlBodyAndPlainTextBodyAndApiTemplateIsEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                HtmlBody = string.Empty,
                PlainTextBody = string.Empty,
                ApiTemplate = null
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasMessageBody(message);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasMessageBody_ReturnsTrue_WhenHtmlBodyAndApiTemplateIsEmptyAndPlainTextBodyIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                HtmlBody = string.Empty,
                PlainTextBody = RandomHelper.RandomString(),
                ApiTemplate = null
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasMessageBody(message);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasMessageBody_ReturnsTrue_WhenPlainTextBodyAndApiTemplateIsNotEmptyAndHtmlBodyIsEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                HtmlBody = RandomHelper.RandomString(),
                PlainTextBody = string.Empty,
                ApiTemplate = null
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasMessageBody(message);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasMessageBody_ReturnsTrue_WhenApiTemplateIsNotEmptyAndPlainTextBodyAndHtmlBodyIsEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                HtmlBody = string.Empty,
                PlainTextBody = string.Empty,
                ApiTemplate = RandomHelper.RandomInt()
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasMessageBody(message);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasMessageBody_ReturnsTrue_WhenHtmlBodyAndApiTemplateIsEmptyAndPlainTextBodyAndAmpBodyIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                HtmlBody = string.Empty,
                PlainTextBody = RandomHelper.RandomString(),
                ApiTemplate = null,
                AmpBody = RandomHelper.RandomString()
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasMessageBody(message);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasMessageBody_ReturnsTrue_WhenPlainTextBodyAndApiTemplateAndAmpBodyIsNotEmptyAndHtmlBodyIsEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                HtmlBody = RandomHelper.RandomString(),
                PlainTextBody = string.Empty,
                ApiTemplate = null,
                AmpBody = RandomHelper.RandomString()
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasMessageBody(message);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasMessageBody_ReturnsTrue_WhenApiTemplateAndAmpBodyIsNotEmptyAndPlainTextBodyAndHtmlBodyIsEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                HtmlBody = string.Empty,
                PlainTextBody = string.Empty,
                ApiTemplate = RandomHelper.RandomInt(),
                AmpBody = RandomHelper.RandomString()
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasMessageBody(message);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasMessageBody_ReturnsFalse_WhenPlainTextBodyAndHtmlBodyAndApiTemplateIsEmptyAndAmpBodyIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                HtmlBody = string.Empty,
                PlainTextBody = string.Empty,
                ApiTemplate = null,
                AmpBody = RandomHelper.RandomString()
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasMessageBody(message);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasMessageBody_ReturnsFalse_WhenPlainTextBodyAndHtmlBodyAndApiTemplateAndAmpBodyIsEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                HtmlBody = string.Empty,
                PlainTextBody = string.Empty,
                ApiTemplate = null,
                AmpBody = string.Empty
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasMessageBody(message);

            //Assert
            Assert.IsFalse(actual);
        }

        #endregion


        #region HasApiTemplate

        [TestMethod]
        public void HasApiTemplate_ReturnsTrue_WhenApiTemplateIsNotZero()
        {
            //Arrange
            var message = new BasicMessage
            {
                ApiTemplate = RandomHelper.RandomInt(1, 10)
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasApiTemplate(message);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasApiTemplate_ReturnsTrue_WhenApiTemplateIsNotMinValue()
        {
            //Arrange
            var message = new BasicMessage
            {
                ApiTemplate = RandomHelper.RandomInt(1, 10)
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasApiTemplate(message);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasApiTemplate_ReturnsFalse_WhenApiTemplateIsZero()
        {
            //Arrange
            var message = new BasicMessage { ApiTemplate = 0 };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasApiTemplate(message);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasApiTemplate_ReturnsFalse_WhenApiTemplateIsMinValue()
        {
            //Arrange
            var message = new BasicMessage { ApiTemplate = int.MinValue };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasApiTemplate(message);

            //Assert
            Assert.IsFalse(actual);
        }

        #endregion


        #region ValidateRecipients(IBasicMessage payload)

        [TestMethod]
        public void ValidateRecipients_BasicMessage_ReturnsNoRecipients_WhenToAndCcAndBccIsNull()
        {
            //Arrange
            var message = new BasicMessage();
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateRecipients(message);

            //Assert
            Assert.AreEqual(SendResult.RecipientValidationNoneInMessage, actual.Result);
        }

        [TestMethod]
        public void ValidateRecipients_BasicMessage_ReturnsSuccess_WhenToIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                To = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateRecipients(message);

            //Assert
            Assert.AreEqual(SendResult.Success, actual.Result);
        }

        [TestMethod]
        public void ValidateRecipients_BasicMessage_ReturnsSuccess_WhenCcIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                Cc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateRecipients(message);

            //Assert
            Assert.AreEqual(SendResult.Success, actual.Result);
        }

        [TestMethod]
        public void ValidateRecipients_BasicMessage_ReturnsSuccess_WhenBccIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                Bcc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateRecipients(message);

            //Assert
            Assert.AreEqual(SendResult.Success, actual.Result);
        }

        [TestMethod]
        public void ValidateRecipients_BasicMessage_ReturnsTooManyRecipients_WhenToAndCcAndBccCombinedHasToManyRecipients()
        {
            //Arrange
            var validator = new SendValidator();
            var message = new BasicMessage
            {
                To = RandomHelper.RandomListOfRecipients(validator.MaximumRecipientsPerMessage / 2),
                Cc = RandomHelper.RandomListOfRecipients(validator.MaximumRecipientsPerMessage / 2),
                Bcc = RandomHelper.RandomListOfRecipients(validator.MaximumRecipientsPerMessage / 2)
            };

            //Act
            var actual = validator.ValidateRecipients(message);

            //Assert
            Assert.AreEqual(SendResult.RecipientValidationMaxExceeded, actual.Result);
        }

        [TestMethod]
        public void ValidateRecipients_BasicMessage_ReturnsTooManyRecipients_WhenToHasToManyRecipients()
        {
            //Arrange
            var validator = new SendValidator();
            var message = new BasicMessage
            {
                To = RandomHelper.RandomListOfRecipients(validator.MaximumRecipientsPerMessage + 1)
            };

            //Act
            var actual = validator.ValidateRecipients(message);

            //Assert
            Assert.AreEqual(SendResult.RecipientValidationMaxExceeded, actual.Result);
        }

        #endregion


        #region ValidateRecipients(IBulkMessage message)

        [TestMethod]
        public void ValidateRecipients_BulkMessage_ReturnsNoRecipients_WhenToIsNull()
        {
            //Arrange
            var message = new BulkMessage { To = null };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateRecipients(message);

            //Assert
            Assert.AreEqual(SendResult.RecipientValidationMissingTo, actual.Result);
        }

        [TestMethod]
        public void ValidateRecipients_BulkMessage_ReturnsNoRecipients_WhenToIsEmpty()
        {
            //Arrange
            var message = new BulkMessage { To = new List<IBulkRecipient>() };
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateRecipients(message);

            //Assert
            Assert.AreEqual(SendResult.RecipientValidationMissingTo, actual.Result);
        }


        [TestMethod]
        public void ValidateRecipients_BulkMessage_ReturnsTooManyRecipients_WhenToHasToManyRecipients()
        {
            //Arrange
            var validator = new SendValidator();
            var message = new BulkMessage
            {
                To = RandomHelper.RandomListOfBulkRecipients(validator.MaximumRecipientsPerMessage + 1)
            };

            //Act
            var actual = validator.ValidateRecipients(message);

            //Assert
            Assert.AreEqual(SendResult.RecipientValidationMaxExceeded, actual.Result);
        }

        #endregion


        #region HasSubject

        [TestMethod]
        public void HasSubject_ReturnsFalse_WhenSubjectIsEmpty()
        {
            //Arrange
            var message = new BasicMessage { Subject = string.Empty };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasSubject(message);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasSubject_ReturnsTrue_WhenSubjectIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage { Subject = RandomHelper.RandomString() };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasSubject(message);

            //Assert
            Assert.IsTrue(actual);
        }

        #endregion


        #region HasFromAddress

        [TestMethod]
        public void HasFromAddress_ReturnsFalse_WhenFromRecipientIsNull()
        {
            //Arrange
            var message = new BasicMessage { From = null };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasFromAddress(message);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasFromAddress_ReturnsFalse_WhenFromRecipientIsEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                From = new EmailAddress(string.Empty)
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasFromAddress(message);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasFromAddress_ReturnsTrue_WhenFromRecipientIsNotEmpty()
        {
            //Arrange
            var message = new BasicMessage
            {
                From = new EmailAddress(RandomHelper.RandomEmail())
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasFromAddress(message);

            //Assert
            Assert.IsTrue(actual);
        }

        #endregion


        #region GetFullRecipientCount

        [TestMethod]
        public void GetFullRecipientCount_BasicMessage_ReturnsGT0_WhenOnlyToRecipientsHasOneValue()
        {
            //Arrange
            var message = new BasicMessage
            {
                To = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.GetFullRecipientCount(message);

            //Assert
            Assert.IsTrue(actual > 0);
        }

        [TestMethod]
        public void GetFullRecipientCount_BasicMessage_ReturnsGT0_WhenOnlyCcRecipientsHasOneValue()
        {
            //Arrange
            var message = new BasicMessage
            {
                Cc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.GetFullRecipientCount(message);

            //Assert
            Assert.IsTrue(actual > 0);
        }

        [TestMethod]
        public void GetFullRecipientCount_BasicMessage_ReturnsGT0_WhenOnlyBccRecipientsHasOneValue()
        {
            //Arrange
            var message = new BasicMessage
            {
                Bcc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.GetFullRecipientCount(message);

            //Assert
            Assert.IsTrue(actual > 0);
        }

        [TestMethod]
        public void GetFullRecipientCount_BasicMessage_Returns3_WhenEachRecipientsHasValue()
        {
            //Arrange
            var message = new BasicMessage
            {
                To = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) },
                Cc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) },
                Bcc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.GetFullRecipientCount(message);

            //Assert
            Assert.IsTrue(actual > 0);
        }

        [TestMethod]
        public void GetFullRecipientCount_BasicMessage_Returns0_WhenNoRecipientsAdded()
        {
            //Arrange
            var message = new BasicMessage();
            var validator = new SendValidator();

            //Act
            var actual = validator.GetFullRecipientCount(message);

            //Assert
            Assert.AreEqual(0, actual);
        }

        #endregion


        #region ValidateCredentials

        [TestMethod]
        public void ValidateCredentials_ReturnsAuthenticationError_WhenServerIdAndApiKeyIsEmpty()
        {
            //Arrange
            const int serverId = int.MinValue;
            var apiKey = string.Empty;
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateCredentials(serverId, apiKey);

            //Assert
            Assert.AreEqual(SendResult.AuthenticationValidationFailed, actual.Result);
        }

        [TestMethod]
        public void ValidateCredentials_ReturnsAuthenticationError_WhenServerIdIsNotEmptyAndApiKeyIsEmpty()
        {
            //Arrange
            var serverId = RandomHelper.RandomServerId();
            var apiKey = string.Empty;
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateCredentials(serverId, apiKey);

            //Assert
            Assert.AreEqual(SendResult.AuthenticationValidationFailed, actual.Result);
        }

        [TestMethod]
        public void ValidateCredentials_ReturnsAuthenticationError_WhenApiKeyIsNotEmptyAndServerIdIsEmpty()
        {
            //Arrange
            const int serverId = int.MinValue;
            var apiKey = RandomHelper.RandomString();
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateCredentials(serverId, apiKey);

            //Assert
            Assert.AreEqual(SendResult.AuthenticationValidationFailed, actual.Result);
        }

        [TestMethod]
        public void ValidateCredentials_ReturnsSuccess_WhenApiKeyAndServerIdIsNotEmpty()
        {
            //Arrange
            var serverId = RandomHelper.RandomServerId();
            var apiKey = RandomHelper.RandomString();
            var validator = new SendValidator();

            //Act
            var actual = validator.ValidateCredentials(serverId, apiKey);

            //Assert
            Assert.AreEqual(SendResult.Success, actual.Result);
        }
        #endregion


        #region HasValidCustomHeaders

        [TestMethod]
        public void HasValidCustomHeaders_ReturnsFalse_WhenKeyAndValueAreEmpty()
        {
            //Arrange
            var customHeaders = new List<ICustomHeader>
            {
                new CustomHeader(string.Empty, string.Empty)
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasValidCustomHeaders(customHeaders);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasValidCustomHeaders_ReturnsFalse_WhenKeyIsNotEmptyAndValueIsEmpty()
        {
            //Arrange
            var customHeaders = new List<ICustomHeader>
            {
                new CustomHeader(RandomHelper.RandomString(), string.Empty)
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasValidCustomHeaders(customHeaders);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasValidCustomHeaders_ReturnsFalse_WhenKeyIsEmptyAndValueIsNotEmpty()
        {
            //Arrange
            var customHeaders = new List<ICustomHeader>
            {
                new CustomHeader(string.Empty, RandomHelper.RandomString())
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasValidCustomHeaders(customHeaders);

            //Assert
            Assert.IsFalse(actual);
        }


        [TestMethod]
        public void HasValidCustomHeaders_ReturnsTrue_WhenDictionaryIsNull()
        {
            //Arrange
            var customHeaders = new List<ICustomHeader>();
            var validator = new SendValidator();

            //Act
            var actual = validator.HasValidCustomHeaders(customHeaders);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasValidCustomHeaders_ReturnsTrue_WhenDictionaryIsValid()
        {
            //Arrange
            var customHeaders = new List<ICustomHeader>
            {
                new CustomHeader(RandomHelper.RandomString(), RandomHelper.RandomString())
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasValidCustomHeaders(customHeaders);

            //Assert
            Assert.IsTrue(actual);
        }

        #endregion

        #region HasValidMetadataHeaders

        [TestMethod]
        public void HasValidMetadataHeaders_ReturnsFalse_WhenKeyAndValueAreEmpty()
        {
            //Arrange
            var customHeaders = new List<IMetadata>
            {
                new Metadata(string.Empty, string.Empty)
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasValidMetadata(customHeaders);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasValidMetadataHeaders_ReturnsFalse_WhenKeyIsNotEmptyAndValueIsEmpty()
        {
            //Arrange
            var customHeaders = new List<IMetadata>
            {
                new Metadata(RandomHelper.RandomString(), string.Empty)
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasValidMetadata(customHeaders);

            //Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasValidMetadataHeaders_ReturnsFalse_WhenKeyIsEmptyAndValueIsNotEmpty()
        {
            //Arrange
            var customHeaders = new List<IMetadata>
            {
                new Metadata(string.Empty, RandomHelper.RandomString())
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasValidMetadata(customHeaders);

            //Assert
            Assert.IsFalse(actual);
        }


        [TestMethod]
        public void HasValidMetadataHeaders_ReturnsTrue_WhenDictionaryIsNull()
        {
            //Arrange
            var customHeaders = new List<IMetadata>();
            var validator = new SendValidator();

            //Act
            var actual = validator.HasValidMetadata(customHeaders);

            //Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void HasValidMetadataHeaders_ReturnsTrue_WhenDictionaryIsValid()
        {
            //Arrange
            var customHeaders = new List<IMetadata>
            {
                new Metadata(RandomHelper.RandomString(), RandomHelper.RandomString())
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasValidMetadata(customHeaders);

            //Assert
            Assert.IsTrue(actual);
        }

        #endregion

        #region HasInvalidRecipients(IBasicMessage message)

        [TestMethod]
        public void HasInvalidRecipients_IBasicMessage_ReturnsListOfOne_WhenToHasOneInvalid()
        {
            //Arrange
            var message = new BasicMessage
            {
                To = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomString()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasInvalidRecipients(message);

            //Assert
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void HasInvalidRecipients_IBasicMessage_ReturnsListOfOne_WhenCcHasOneInvalid()
        {
            //Arrange
            var message = new BasicMessage
            {
                Cc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomString()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasInvalidRecipients(message);

            //Assert
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void HasInvalidRecipients_IBasicMessage_ReturnsListOfOne_WhenBccHasOneInvalid()
        {
            //Arrange
            var message = new BasicMessage
            {
                Bcc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomString()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasInvalidRecipients(message);

            //Assert
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void HasInvalidRecipients_IBasicMessage_ReturnsListOfThree_WhenEachRecipientHasOneInvalid()
        {
            //Arrange
            var message = new BasicMessage
            {
                To = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomString()) },
                Cc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomString()) },
                Bcc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomString()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasInvalidRecipients(message);

            //Assert
            Assert.AreEqual(3, actual.Count);
        }

        [TestMethod]
        public void HasInvalidRecipients_IBasicMessage_ReturnsNull_WhenNoInvalidRecipientsFound()
        {
            //Arrange
            var message = new BasicMessage
            {
                To = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) },
                Cc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) },
                Bcc = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasInvalidRecipients(message);

            //Assert
            Assert.AreEqual(0, actual.Count);
        }

        #endregion


        #region HasInvalidRecipients(IBulkMessage message)

        [TestMethod]
        public void HasInvalidRecipients_IBulkMessage_ReturnsListOfOne_WhenToHasOneInvalid()
        {
            //Arrange
            var message = new BulkMessage
            {
                To = new List<IBulkRecipient> { new BulkRecipient(RandomHelper.RandomString()) }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasInvalidRecipients(message);

            //Assert
            Assert.AreEqual(1, actual.Count);
        }

        [TestMethod]
        public void HasInvalidRecipients_IBasicMessage_ReturnsListOfThree_WhenToHasThreeInvalid()
        {
            //Arrange
            var message = new BulkMessage
            {
                To = new List<IBulkRecipient>
                {
                    new BulkRecipient(RandomHelper.RandomString()),
                    new BulkRecipient(RandomHelper.RandomString()),
                    new BulkRecipient(RandomHelper.RandomString())
                }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasInvalidRecipients(message);

            //Assert
            Assert.AreEqual(3, actual.Count);
        }

        [TestMethod]
        public void HasInvalidRecipients_IBulkMessage_ReturnsNull_WhenNoInvalidRecipientsFound()
        {
            //Arrange
            var message = new BulkMessage
            {
                To = new List<IBulkRecipient>
                {
                    new BulkRecipient(RandomHelper.RandomEmail()),
                    new BulkRecipient(RandomHelper.RandomEmail()),
                    new BulkRecipient(RandomHelper.RandomEmail())
                }
            };
            var validator = new SendValidator();

            //Act
            var actual = validator.HasInvalidRecipients(message);

            //Assert
            Assert.AreEqual(0, actual.Count);
        }

        #endregion


        #region FindInvalidRecipients(IList<IEmailAddress> recipient)

        [TestMethod]
        public void FindInvalidRecipients_ListOfEmailAddress_ReturnsNull_WhenRecipientsIsNull()
        {
            //Arrange
            var validator = new SendValidator();

            //Act
            var actual = validator.FindInvalidRecipients((List<IEmailAddress>)null);

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void FindInvalidRecipients_ListOfEmailAddress_ReturnsNull_WhenRecipientsIsEmpty()
        {
            //Arrange
            var message = new List<IEmailAddress>();
            var validator = new SendValidator();

            //Act
            var actual = validator.FindInvalidRecipients(message);

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void FindInvalidRecipients_ListOfEmailAddress_ReturnsNull_WhenNoInvalidRecipientsFound()
        {
            //Arrange
            var message = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomEmail()) };
            var validator = new SendValidator();

            //Act
            var actual = validator.FindInvalidRecipients(message);

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void FindInvalidRecipients_ListOfEmailAddress_ReturnsList_WhenInvalidRecipientsFound()
        {
            //Arrange
            var message = new List<IEmailAddress> { new EmailAddress(RandomHelper.RandomString()) };
            var validator = new SendValidator();

            //Act
            var actual = validator.FindInvalidRecipients(message);

            //Assert
            Assert.AreEqual(1, actual.Count);
        }

        #endregion


        #region HasInvalidRecipients(BulkRecipient message)

        [TestMethod]
        public void FindInvalidRecipients_ListOfBulkRecipient_ReturnsNull_WhenRecipientsIsNull()
        {
            //Arrange
            var validator = new SendValidator();

            //Act
            var actual = validator.FindInvalidRecipients((List<IBulkRecipient>)null);

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void FindInvalidRecipients_ListOfBulkRecipient_ReturnsNull_WhenRecipientsIsEmpty()
        {
            //Arrange
            var message = new List<IBulkRecipient>();
            var validator = new SendValidator();

            //Act
            var actual = validator.FindInvalidRecipients(message);

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void FindInvalidRecipients_ListOfBulkRecipient_ReturnsNull_WhenNoInvalidRecipientsFound()
        {
            //Arrange
            var message = new List<IBulkRecipient> { new BulkRecipient(RandomHelper.RandomEmail()) };
            var validator = new SendValidator();

            //Act
            var actual = validator.FindInvalidRecipients(message);

            //Assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void FindInvalidRecipients_ListOfBulkRecipient_ReturnsList_WhenInvalidRecipientsFound()
        {
            //Arrange
            var message = new List<IBulkRecipient> { new BulkRecipient(RandomHelper.RandomString()) };
            var validator = new SendValidator();

            //Act
            var actual = validator.FindInvalidRecipients(message);

            //Assert
            Assert.AreEqual(1, actual.Count);
        }

        #endregion
    }
}
