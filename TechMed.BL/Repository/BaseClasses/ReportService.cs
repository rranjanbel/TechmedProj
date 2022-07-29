
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using SelectPdf;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;

namespace TechMed.API.Services
{
    public class ReportService : IReportService
    {
        //private readonly IConverter _converter;
        private readonly IPatientCaseRepository _patientCaeRepository;
        public ReportService(IPatientCaseRepository patientCaeRepository)
        {
            _patientCaeRepository = patientCaeRepository;
            //_converter = converter;
        }
        public async Task<byte[]> GeneratePdfReport(Int64 PatientCaseID, string contentRootPath)
        {
            var patientCaseVM = await _patientCaeRepository.GetPatientCaseDetailsByCaseID(PatientCaseID, contentRootPath);
            string relativePath = @"\\MyStaticFiles\\";
            string path = contentRootPath + relativePath;

            string MPGovLogo = path + @"MPGovLogo.png";
            string MPArogyam = path + @"MPArogyam.jpg";
            string NHMLogo = path + @"NHMLogo.png";

            var html = $@"";
            html = File.ReadAllText(path + @"PrescriptionPreviewPdf.html");
            var Footer = $@"";
            Footer = File.ReadAllText(path + @"footer.html");

            html = html.Replace("{{MPGovLogo}}", MPGovLogo);
            html = html.Replace("{{MPArogyam}}", MPArogyam);
            html = html.Replace("{{NHMLogo}}", NHMLogo);

            html = html.Replace("{{phcName}}", patientCaseVM.PHCName);
            html = html.Replace("{{district}}", patientCaseVM.patientMaster.District);
            html = html.Replace("{{city}}", patientCaseVM.patientMaster.Block);
            html = html.Replace("{{state}}", patientCaseVM.patientMaster.State);

            html = html.Replace("{{doctorName}}", patientCaseVM.DoctorName);
            html = html.Replace("{{doctorMobileNo}}", patientCaseVM.DoctorMobileNo);
            html = html.Replace("{{doctorMCINo}}", patientCaseVM.DoctorMCINo);
            html = html.Replace("{{doctorQalification}}", patientCaseVM.DoctorQalification);
            html = html.Replace("{{doctorSpecialization}}", patientCaseVM.DoctorSpecialization);

            html = html.Replace("{{firstName}}", patientCaseVM.patientMaster.FirstName);
            html = html.Replace("{{lastName}}", patientCaseVM.patientMaster.LastName);
            html = html.Replace("{{age}}", UtilityMaster.GetAgeOfPatient(patientCaseVM.patientMaster.Dob));
            html = html.Replace("{{gender}}", patientCaseVM.patientMaster.GenderId == 1 ? "Male" : patientCaseVM.patientMaster.GenderId == 2 ? "Female" : "Other");
            html = html.Replace("{{mobileNo}}", patientCaseVM.patientMaster.MobileNo);
            html = html.Replace("{{guardianName}}", patientCaseVM.patientMaster.GuardianName);
            html = html.Replace("{{address}}", patientCaseVM.patientMaster.Address);
            html = html.Replace("{{createdOn}}", Convert.ToDateTime(patientCaseVM.patientMaster.CreatedOn).ToString("dd/MM/yyyy HH:mm:ss"));
            html = html.Replace("{{patientId}}", patientCaseVM.patientMaster.PatientId.ToString());
            html = html.Replace("{{opdno}}", patientCaseVM.patientCase.Opdno);
            html = html.Replace("{{caseFileNumber}}", patientCaseVM.patientCase.CaseFileNumber);

            string htmlVitalTemplate = File.ReadAllText(path + @"Vital.html");
            string htmlVital = "";
            patientCaseVM.vitals = patientCaseVM.vitals.Where(a => a.Value != null && a.Value != "").ToList();
            //var t = Partition<PatientCaseVitalsVM>(patientCaseVM.vitals, 3);

            //var List1 = t[0];
            //var List2 = t[1];
            //var List3 = t[2];
            if (patientCaseVM.vitals!=null)
            {
                for (int i = 0; i < patientCaseVM.vitals.Count; i++)
                {
                    string unit = (patientCaseVM.vitals[i].Unit.ToLower().Trim() == "string" ? "" : patientCaseVM.vitals[i].Unit.ToLower().Trim() == "bool" ? "" : patientCaseVM.vitals[i].Unit);
                    if (string.IsNullOrEmpty(unit))unit = unit.Trim('[').Trim(']');
                    else unit = "[" + unit.Trim() + "]";
                    htmlVital = htmlVital + htmlVitalTemplate;
                    if (i == patientCaseVM.vitals.Count)
                    {

                        htmlVital = htmlVital.Replace("{{vitalName1}}", "");
                        htmlVital = htmlVital.Replace("{{vitalValue1}}", "");
                        htmlVital = htmlVital.Replace("{{vitalName2}}", "");
                        htmlVital = htmlVital.Replace("{{vitalValue2}}", "");
                        htmlVital = htmlVital.Replace("{{vitalName3}}", "");
                        htmlVital = htmlVital.Replace("{{vitalValue3}}", "");
                        break;
                    }
                    else
                    {
                        //write1
                        unit = (patientCaseVM.vitals[i].Unit.ToLower().Trim() == "string" ? "" : patientCaseVM.vitals[i].Unit.ToLower().Trim() == "bool" ? "" : patientCaseVM.vitals[i].Unit);
                        if (string.IsNullOrEmpty(unit)) unit = unit.Trim('[').Trim(']');
                        else unit = "[" + unit.Trim() + "]";

                        htmlVital = htmlVital.Replace("{{vitalName1}}", patientCaseVM.vitals[i].VitalName + " " + unit);
                        htmlVital = htmlVital.Replace("{{vitalValue1}}", patientCaseVM.vitals[i].Value);
                        i++;
                    }
                    if (i == patientCaseVM.vitals.Count)
                    {
                        htmlVital = htmlVital.Replace("{{vitalName2}}", "");
                        htmlVital = htmlVital.Replace("{{vitalValue2}}", "");
                        htmlVital = htmlVital.Replace("{{vitalName3}}", "");
                        htmlVital = htmlVital.Replace("{{vitalValue3}}", "");
                        break;
                    }
                    else
                    {
                        //write1
                        unit = (patientCaseVM.vitals[i].Unit.ToLower().Trim() == "string" ? "" : patientCaseVM.vitals[i].Unit.ToLower().Trim() == "bool" ? "" : patientCaseVM.vitals[i].Unit);
                        if (string.IsNullOrEmpty(unit)) unit = unit.Trim('[').Trim(']');
                        else unit = "[" + unit.Trim() + "]";
                        htmlVital = htmlVital.Replace("{{vitalName2}}", patientCaseVM.vitals[i].VitalName + " " + unit);
                        htmlVital = htmlVital.Replace("{{vitalValue2}}", patientCaseVM.vitals[i].Value);
                        i++;
                    }
                    if (i == patientCaseVM.vitals.Count)
                    {
                        htmlVital = htmlVital.Replace("{{vitalName3}}", "");
                        htmlVital = htmlVital.Replace("{{vitalValue3}}", "");
                        break;
                    }
                    else
                    {
                        //write1
                        unit = (patientCaseVM.vitals[i].Unit.ToLower().Trim() == "string" ? "" : patientCaseVM.vitals[i].Unit.ToLower().Trim() == "bool" ? "" : patientCaseVM.vitals[i].Unit);
                        if (string.IsNullOrEmpty(unit)) unit = unit.Trim('[').Trim(']');
                        else unit = "[" + unit.Trim() + "]";
                        htmlVital = htmlVital.Replace("{{vitalName3}}", patientCaseVM.vitals[i].VitalName + " " + unit);
                        htmlVital = htmlVital.Replace("{{vitalValue3}}", patientCaseVM.vitals[i].Value);
                        i++;
                    }
                }
            }
           

            //foreach (var vital in patientCaseVM.vitals)
            //{
            //    htmlVital = htmlVital + htmlVitalTemplate;
            //    htmlVital = htmlVital.Replace("{{vitalName}}", vital.VitalName);
            //    htmlVital = htmlVital.Replace("{{vitalValue}}", vital.Value);
            //}
            html = html.Replace("{{vitaltr}}", htmlVital);


            html = html.Replace("{{symptom}}", patientCaseVM.patientCase.Symptom);
            //html = html.Replace("{{diagnostic}}", patientCaseVM.patientCase.SuggestedDiagnosis);
            html = html.Replace("{{provisionalDiagnosis}}", patientCaseVM.patientCase.ProvisionalDiagnosis);
            html = html.Replace("{{instruction}}", patientCaseVM.patientCase.Instruction);
            html = html.Replace("{{findings}}", patientCaseVM.patientCase.Finding);

            string ExaminationTemplate = @"<tr><td>{{diagnostic}}</td></tr>";
            string Examination = "";

            foreach (var item in patientCaseVM.caseDiagnosisTestList)
            {
                Examination = Examination + ExaminationTemplate;
                Examination = Examination.Replace("{{diagnostic}}", item.DiagonosticTestName);
            }

            html = html.Replace("{{Examination}}", Examination);
            html = html.Replace("{{reviewDate}}", patientCaseVM.patientCase.ReviewDate == null ? "NA" : Convert.ToDateTime(patientCaseVM.patientCase.ReviewDate).ToString("dd/MMM/yyyy"));
            //html = html.Replace("{{createdOndateTime}}", Convert.ToDateTime(patientCaseVM.patientCase.CreatedOn).ToString("dd/MMM/yyyy HH:mm:ss"));
            Footer = Footer.Replace("{{createdOndateTime}}", Convert.ToDateTime(patientCaseVM.patientCase.CreatedOn).ToString("dd/MMM/yyyy HH:mm:ss"));

            string MedicineTemplate = "  <tr>                    <td style=\"text-align:left\">{{medicineName}}</td>                    <td style=\"text-align:center\">{{Quantity}}</td>                    <td style=\"text-align:center\">{{dose}}</td>                    <td style=\"text-align:center\">{{duration}}</td>                    <td style=\"text-align:left\">{{doseDuration}}</td>                </tr>";
            string Medicine = "";
            foreach (var item in patientCaseVM.caseMedicineList)
            {
                Medicine = Medicine + MedicineTemplate;
                Medicine = Medicine.Replace("{{medicineName}}", item.DrugName);
                Medicine = Medicine.Replace("{{Quantity}}", (item.Duration * (item.Od == true ? 1 : (item.Bd == true ? 2 : (item.Td == true ? 3 : (item.Qid == true ? 4 : 0))))).ToString());
                Medicine = Medicine.Replace("{{dose}}", (item.Od == true ? "O.D" : " ") + (item.Bd == true ? "B.I.D" : " ") + (item.Td == true ? "T.D.S" : " ") + (item.Qid == true ? "Q.I.D" : " "));
                Medicine = Medicine.Replace("{{duration}}", item.Duration.ToString());
                Medicine = Medicine.Replace("{{doseDuration}}", (item.EmptyStomach == true ? "EmptyStomach" : " ") + (item.AfterMeal == true ? "AfterMeal" : " "));
            }
            html = html.Replace("{{medicine}}", Medicine);
            html = html.Replace("{{doctorSignature}}", patientCaseVM.DoctorSignature);

            //GlobalSettings globalSettings = new GlobalSettings();
            //globalSettings.ColorMode = ColorMode.Color;
            //globalSettings.Orientation = Orientation.Portrait;
            //globalSettings.PaperSize = PaperKind.A4;
            //globalSettings.Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 0, Right = 0 };
            //globalSettings.PageOffset = 0;

            //ObjectSettings objectSettings = new ObjectSettings();
            //objectSettings.PagesCount = true;
            //objectSettings.HtmlContent = html;
            //WebSettings webSettings = new WebSettings();
            //webSettings.DefaultEncoding = "utf-8";
            //HeaderSettings headerSettings = new HeaderSettings();
            //headerSettings.FontSize = 10;
            //headerSettings.FontName = "Ariel";
            //headerSettings.Right = "Page [page] of [toPage]";
            //headerSettings.Line = true;
            //FooterSettings footerSettings = new FooterSettings();
            //footerSettings.FontSize = 10;
            //footerSettings.FontName = "Ariel";
            //footerSettings.Center = "";
            //footerSettings.Line = true;
            //objectSettings.HeaderSettings = headerSettings;
            //objectSettings.FooterSettings = footerSettings;
            //objectSettings.WebSettings = webSettings;
            //HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            //{
            //    GlobalSettings = globalSettings,
            //    Objects = { objectSettings },
            //};

            var testFilePath = "path/to/test.pdf";
            var testFileBytes = BtnCreatePdf_Click(html, Footer);
            //PdfSharpConvert(html);
            //var testFileBytes = _converter.Convert(htmlToPdfDocument);

            using (var ms = new MemoryStream(testFileBytes))
            {

                List<CaseDocumentBase64VM> caseDocuments = new List<CaseDocumentBase64VM>();
                caseDocuments.Add(new CaseDocumentBase64VM
                {
                    DocumentTypeId = 2,
                    file = Convert.ToBase64String(testFileBytes),
                    name = PatientCaseID.ToString()+"_Case_Prescription",
                    patientCaseId = PatientCaseID
                });
                var result = _patientCaeRepository.UploadCaseDocFromByte(caseDocuments, contentRootPath);
            }
            //return _converter.Convert(htmlToPdfDocument);
            return testFileBytes;
        }
        public static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }
        public static List<T>[] Partition<T>(List<T> list, int totalPartitions)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (totalPartitions < 1)
                throw new ArgumentOutOfRangeException("totalPartitions");

            List<T>[] partitions = new List<T>[totalPartitions];

            int maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            int k = 0;

            for (int i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<T>();
                for (int j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                        break;
                    partitions[i].Add(list[j]);
                }
                k += maxSize;
            }

            return partitions;
        }



        protected byte[] BtnCreatePdf_Click(string htmlString,string footerString="this is header")
        {
            // read parameters from the webpage


            //string pdf_page_size = DdlPageSize.SelectedValue;
            //PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
            //    pdf_page_size, true);

            //string pdf_orientation = DdlPageOrientation.SelectedValue;
            //PdfPageOrientation pdfOrientation =
            //    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
            //    pdf_orientation, true);

            int webPageWidth = 1024;
            try
            {
                webPageWidth = Convert.ToInt32(1000);
            }
            catch { }

            int webPageHeight = 0;
            try
            {
                webPageHeight = Convert.ToInt32(1000);
            }
            catch { }

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();



            converter.Options.DisplayFooter = true;
            converter.Footer.DisplayOnFirstPage = true;
            converter.Footer.DisplayOnOddPages = true;
            converter.Footer.DisplayOnEvenPages = true;
            converter.Footer.Height = 50;
            PdfHtmlSection headerHtml = new PdfHtmlSection(footerString,"");
            headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Footer.Add(headerHtml);

            // set converter options
            //converter.Options.PdfPageSize = pageSize;
            //converter.Options.PdfPageOrientation = pdfOrientation;
            //converter.Options.WebPageWidth = webPageWidth;
            //converter.Options.WebPageHeight = webPageHeight;
            converter.Options.DisplayHeader = true;
            converter.Options.DisplayFooter = true;
            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(htmlString);
            doc.Pages.Remove(doc.Pages[0]);

            // save pdf document
            //doc.Save("Sample.pdf");
            var bytefile = doc.Save();
            // close pdf document
            doc.Close();
            return bytefile;

        }

        //public Byte[] PdfSharpConvert(String html)
        //{
        //    Byte[] res = null;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.Royal);
        //        //pdf.Save(ms);
        //        pdf.Save("file.pdf");
        //        res = ms.ToArray();

        //    }
        //    return res;
        //}
    }
}
