﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web;
namespace CarbonDash.Core 
{


public static class MailHelper
{
   /// <summary>
   /// Sends an mail message
   /// </summary>
   /// <param name="from">Sender address</param>
   /// <param name="to">Recepient address</param>
   /// <param name="bcc">Bcc recepient</param>
   /// <param name="cc">Cc recepient</param>
   /// <param name="subject">Subject of mail message</param>
   /// <param name="body">Body of mail message</param>
   ///    
   public static void SendSimple(string to, string subject, string body)
   {
       string from = SiteSettings.FROM_EMAIL;
       MailHelper.SendMailMessage(from, to, "","",subject, body);
   }
   public static void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
   {
      // Instantiate a new instance of MailMessage
      MailMessage mMailMessage = new MailMessage();

      // Set the sender address of the mail message
      mMailMessage.From = new MailAddress(from);
      // Set the recepient address of the mail message
      mMailMessage.To.Add(new MailAddress(to));

      // Check if the bcc value is null or an empty string
      if ((bcc != null) && (bcc != string.Empty))
      {
         // Set the Bcc address of the mail message
         mMailMessage.Bcc.Add(new MailAddress(bcc));
      }      // Check if the cc value is null or an empty value
      if ((cc != null) && (cc != string.Empty))
      {
         // Set the CC address of the mail message
         mMailMessage.CC.Add(new MailAddress(cc));
      }       // Set the subject of the mail message
      mMailMessage.Subject = subject;
      // Set the body of the mail message
      mMailMessage.Body = body;

      // Set the format of the mail message body as HTML
      mMailMessage.IsBodyHtml = true;
      // Set the priority of the mail message to normal
      mMailMessage.Priority = MailPriority.Normal;

      // Instantiate a new instance of SmtpClient
      SmtpClient mSmtpClient = new SmtpClient("localhost");
      mSmtpClient.UseDefaultCredentials = false; 
      // Send the mail message
      try
      {

          mSmtpClient.Send(mMailMessage);

      }
      catch (Exception ex)
      {
          HttpContext.Current.Response.Write("<br>from: " + from);
          HttpContext.Current.Response.Write("<br>To: " + to);
          
          HttpContext.Current.Response.Write("<br><br>Subject: " + mMailMessage.Subject);
          HttpContext.Current.Response.Write("<br>Body: " + mMailMessage.Body);

          HttpContext.Current.Response.Write("<br><br><br>Mail: " + ex.Message);

      }
   }
}
}
