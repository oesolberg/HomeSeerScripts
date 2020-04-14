Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime

Sub Main(parm as object)
  Dim sFileName As String
  Dim mail As New MailMessage()
  Dim htmlView As AlternateView
  Dim plainView As AlternateView
  Dim now As DateTime
 
  ' Get Timestamp
  now = DateTime.Now
  sFileName = "waterpressure_" & now.Year & now.Month.ToString() & now.Day.ToString() & "-" & now.Hour.ToString() + now.Minute.ToString() & ".jpg"


  ' Take pictures
  //hs.GetURLImage("<hostname of camera>","<url to take snapshot>",false,80,"/html/snapshots/" & sFileName)
 
 
  ' Set the addresses
  mail.From = New MailAddress("oesolberg@hotmail.com")
  mail.To.Add("<oesolberg@hotmail.com>")

  ' Set the subject
  mail.Subject = "Varmtvannstank"

  ' Create the text only part for those readers not able to view HTML with embedded images (only valid when your homeseer server is accessable from where you receive your email)
  //plainView = AlternateView.CreateAlternateViewFromString("Vedlagt er bilde av målere av vanntrykk" & vbCrLf & "https://<hostname of homeseer>/snapshots/" & sFilename, Nothing, "text/plain")

  ' Create the HTML mail with the embedded photo
  htmlView = AlternateView.CreateAlternateViewFromString("Wie staat er voor de deur?<br /><img src=cid:deurbel>", Nothing, "text/html")

  Dim imageView As New AlternateView(hs.GetAppPath & "/html/snapshots/" & sFileName, MediaTypeNames.Image.Jpeg)
  imageView.ContentId = "deurbel"
  imageView.TransferEncoding = TransferEncoding.Base64

  Dim data As New Attachment(imageView.ContentStream, MediaTypeNames.Image.Jpeg)
  data.ContentDisposition.Inline = true
  data.ContentId = "deurbel"
  data.TransferEncoding = TransferEncoding.Base64
  data.Name = hs.GetAppPath & "/html/snapshots/" & sFileName

  ' Add the views
  mail.AlternateViews.Add(plainView)
  mail.AlternateViews.Add(htmlView)
  mail.Attachments.Add(data)

  ' Send the message
  Dim smtp As New SmtpClient("<hostname of SMTP mail server>")
  smtp.Send(mail)

End Sub