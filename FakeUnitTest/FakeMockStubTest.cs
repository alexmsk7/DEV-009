using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace FakeMockStub.Tests
{
    [TestFixture]
    public class ReportServiceTests
    {
        private ReportService _reportService;
        private Mock<IReportGenerator> _reportGenerator;
        private Mock<IReportSender> _reportSender;

        [SetUp]
        public void SetUp()
        {
            _reportGenerator = new Mock<IReportGenerator>();

            _reportSender = new Mock<IReportSender>();

            _reportService = new ReportService(_reportGenerator.Object, _reportSender.Object);
        }

        [Test]
        public void ProcessReports_WhenCalled_ReturnsNumberOfProcessReports()
        {
            int numberOfReports = _reportService.ProcessReports();

            Assert.That(numberOfReports, Is.EqualTo(0));
        }

        [Test]
        public void ProcessReports_WhenCalled_ReturnsNumberOfCreatedReports()
        {
            _reportGenerator.Setup(x => x.Generate()).Returns(new List<Report>
            {
                new Report(),
                new Report(),
                new Report(),
            });

            int numberOfReports = _reportService.ProcessReports();

            Assert.That(numberOfReports, Is.EqualTo(3));
        }

        [Test]
        public void ProcessReports_SomeReportsAreGenerated_SendAllReports()
        {
            var report1 = new Report();
            var report2 = new Report();


            _reportGenerator.Setup(x => x.Generate()).Returns(new List<Report>
            {
                report1,
                report2,
            });


            int numberOfReports = _reportService.ProcessReports();

            _reportSender.Verify(x=>x.Send(report1, "Manager"), Times.Once);
            _reportSender.Verify(x => x.Send(report2, "Manager"), Times.Once);

        }

        [Test]
        public void ProcessReports_WhenCalled_GenerateReports()
        {
            _reportService.ProcessReports();

            _reportGenerator.Verify(x=>x.Generate(), Times.Once);
        }

        [Test]
        public void ProcessReports_NoGeneratedReports_GenerateSpecialReports()
        {

            _reportGenerator.Setup(x => x.Generate()).Returns(new List<Report>
            {
            });

            int numberOfReports = _reportService.ProcessReports();
           
            _reportSender.Verify(x => x.Send(It.IsAny<SpecialReport>(), "Manager"), Times.Once);

        }

        [Test]
        public void ProcessReports_SendEachSecondReportToAuditors_SendReports()
        {
            _reportGenerator.Setup(x => x.Generate()).Returns(new List<Report>
            {
                new Report(),
                new Report(),
                new Report(),
                new Report(),
                new Report(),
                new Report(),
            });

            int numberOfReports = _reportService.ProcessReports();

            _reportSender.Verify(x => x.Send(It.IsAny<AuditorsReport>(), "Auditor"), Times.Exactly(3));
        }
    }
}
