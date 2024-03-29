/*
 * Copyright 2014 Alastair Wyse (https://github.com/alastairwyse/ApplicationLogging/)
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#pragma warning disable 1591

using System;
using NUnit.Framework;
using NSubstitute;

namespace ApplicationLogging.UnitTests
{
    /// <summary>
    /// Unit tests for class ApplicationLogging.FileApplicationLogger.
    /// </summary>
    [TestFixture]
    public class FileApplicationLoggerTests
    {
        private IStreamWriter mockStreamWriter;
        private FileApplicationLogger testFileApplicationLogger;

        [SetUp]
        protected void SetUp()
        {
            mockStreamWriter = Substitute.For<IStreamWriter>();
            testFileApplicationLogger = new FileApplicationLogger(LogLevel.Debug, ':', "  ", mockStreamWriter);
        }

        [Test]
        public void LogSuccessTests()
        {
            string expectedStackTraceText = Environment.NewLine + @"     at ApplicationLogging.UnitTests.LogSuccessTests() in C:\Development\C#\ApplicationLogging\ApplicationLogging.UnitTests\FileApplicationLoggerTests.cs:line 123" + Environment.NewLine + @"     at ApplicationLoggingUnitTests.LogSuccessTests() in C:\Development\C#\ApplicationLogging\ApplicationLogging.UnitTests\FileApplicationLoggerTests.cs:line 456";

            testFileApplicationLogger.Log(LogLevel.Critical, "Log Text 1.");
            testFileApplicationLogger.Log(123, LogLevel.Error, "Log Text 2.");
            testFileApplicationLogger.Log(LogLevel.Warning, "Log Text 3.", new CustomStackTraceException("Mocked Exception."));
            testFileApplicationLogger.Log(124, LogLevel.Information, "Log Text 4.", new CustomStackTraceException("Mocked Exception 2."));
            testFileApplicationLogger.Log(this, LogLevel.Critical, "Log Text 5.");
            testFileApplicationLogger.Log(this, 123, LogLevel.Error, "Log Text 6.");
            testFileApplicationLogger.Log(this, LogLevel.Warning, "Log Text 7.", new CustomStackTraceException("Mocked Exception 3."));
            testFileApplicationLogger.Log(this, 124, LogLevel.Information, "Log Text 8.", new CustomStackTraceException("Mocked Exception 4."));

            Received.InOrder(() =>
            {
                mockStreamWriter.Received(1).WriteLine(Arg.Is<string>(value => value.Contains(" CRITICAL : Log Text 1.")));
                mockStreamWriter.Received(1).Flush();
                mockStreamWriter.Received(1).WriteLine(Arg.Is<string>(value => value.Contains(" Log Event Id = 123 : ERROR : Log Text 2.")));
                mockStreamWriter.Received(1).Flush();
                mockStreamWriter.Received(1).WriteLine(Arg.Is<string>(value => value.Contains(" WARNING : Log Text 3." + Environment.NewLine + "  ApplicationLogging.UnitTests.CustomStackTraceException: Mocked Exception." + expectedStackTraceText)));
                mockStreamWriter.Received(1).Flush();
                mockStreamWriter.Received(1).WriteLine(Arg.Is<string>(value => value.Contains(" Log Event Id = 124 : Log Text 4." + Environment.NewLine + "  ApplicationLogging.UnitTests.CustomStackTraceException: Mocked Exception 2." + expectedStackTraceText)));
                mockStreamWriter.Received(1).Flush();
                mockStreamWriter.Received(1).WriteLine(Arg.Is<string>(value => value.Contains(" Source = FileApplicationLoggerTests : CRITICAL : Log Text 5.")));
                mockStreamWriter.Received(1).Flush();
                mockStreamWriter.Received(1).WriteLine(Arg.Is<string>(value => value.Contains(" Source = FileApplicationLoggerTests : Log Event Id = 123 : ERROR : Log Text 6.")));
                mockStreamWriter.Received(1).Flush();
                mockStreamWriter.Received(1).WriteLine(Arg.Is<string>(value => value.Contains(" Source = FileApplicationLoggerTests : WARNING : Log Text 7." + Environment.NewLine + "  ApplicationLogging.UnitTests.CustomStackTraceException: Mocked Exception 3." + expectedStackTraceText)));
                mockStreamWriter.Received(1).Flush();
                mockStreamWriter.Received(1).WriteLine(Arg.Is<string>(value => value.Contains(" Source = FileApplicationLoggerTests : Log Event Id = 124 : Log Text 8." + Environment.NewLine + "  ApplicationLogging.UnitTests.CustomStackTraceException: Mocked Exception 4." + expectedStackTraceText)));
                mockStreamWriter.Received(1).Flush();
            });
        }

        [Test]
        public void LogBelowMinimumLogLevelNotLogged()
        {
            testFileApplicationLogger = new FileApplicationLogger(LogLevel.Warning, ':', "  ", mockStreamWriter);

            testFileApplicationLogger.Log(LogLevel.Information, "Log Text 1.");
            testFileApplicationLogger.Log(123, LogLevel.Information, "Log Text 2.");
            testFileApplicationLogger.Log(LogLevel.Information, "Log Text 3.", new CustomStackTraceException("Mocked Exception."));
            testFileApplicationLogger.Log(124, LogLevel.Information, "Log Text 4.", new CustomStackTraceException("Mocked Exception 2."));
            testFileApplicationLogger.Log(LogLevel.Debug, "Log Text 1.");
            testFileApplicationLogger.Log(123, LogLevel.Debug, "Log Text 2.");
            testFileApplicationLogger.Log(LogLevel.Debug, "Log Text 3.", new CustomStackTraceException("Mocked Exception."));
            testFileApplicationLogger.Log(124, LogLevel.Debug, "Log Text 4.", new CustomStackTraceException("Mocked Exception 2."));
            testFileApplicationLogger.Log(this, LogLevel.Information, "Log Text 5.");
            testFileApplicationLogger.Log(this, 123, LogLevel.Information, "Log Text 6.");
            testFileApplicationLogger.Log(this, LogLevel.Information, "Log Text 7.", new CustomStackTraceException("Mocked Exception 3."));
            testFileApplicationLogger.Log(this, 124, LogLevel.Information, "Log Text 8.", new CustomStackTraceException("Mocked Exception 4."));
            testFileApplicationLogger.Log(this, LogLevel.Debug, "Log Text 5.");
            testFileApplicationLogger.Log(this, 123, LogLevel.Debug, "Log Text 6.");
            testFileApplicationLogger.Log(this, LogLevel.Debug, "Log Text 7.", new CustomStackTraceException("Mocked Exception 3."));
            testFileApplicationLogger.Log(this, 124, LogLevel.Debug, "Log Text 8.", new CustomStackTraceException("Mocked Exception 4."));

            mockStreamWriter.DidNotReceiveWithAnyArgs().WriteLine(default);
            mockStreamWriter.DidNotReceiveWithAnyArgs().Flush();
        }

        [Test]
        public void CloseSuccessTest()
        {
            testFileApplicationLogger.Close();

            mockStreamWriter.Received(1).Close();
        }
    }
}
