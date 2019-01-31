using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using IEnumerable = System.Collections.IEnumerable;

namespace FakeMockStub
{
    public class ReportService
    {
        private IReportGenerator _reportGenerator;
        private IReportSender _reportSender;
        public ReportService(IReportGenerator reportGeneratorObject, IReportSender reportSender)
        {
            _reportGenerator = reportGeneratorObject;
            _reportSender = reportSender;
        }

        public int ProcessReports()
        {
           var reports = _reportGenerator.Generate();
            if (!reports.Any())
            {
                var reportSpecial = _reportGenerator.GenerateSpecialReport();
                _reportSender.Send(reportSpecial, "Manager");
            }
            else
            {
                foreach (var report in reports)
                {
                    _reportSender.Send(report, "Manager");
                }
            }

            return  reports.Count();
            //return _reportGenerator.Generate().Count();
        }
    }


    public class Report
    {
    }

    public class SpecialReport : Report
    {
    }

    public interface IReportGenerator
    {
        IEnumerable<Report> Generate();
        SpecialReport GenerateSpecialReport();
    }

    public interface IReportSender
    {
        void Send(Report isAny, string Employee);
    }

    public class AuditorsReport : Report
    {
    }
}
