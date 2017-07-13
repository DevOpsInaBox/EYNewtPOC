using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using Naos.WinRM;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Net.Mail;
using System.Threading;

namespace Automation_Portal
{
    public partial class PortalHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                portalBean formBean = new portalBean();
                if (TryUpdateModel(formBean, new FormValueProvider(ModelBindingExecutionContext)))
                {
                    /**System.Diagnostics.Debug.Write(formBean);
                    var machineManager = new MachineManager("157.56.180.236", "newtdemo", new NetworkCredential("", "Newt!dEmo123").SecurePassword, true);
                    System.Diagnostics.Debug.Write("Machine mgr:");
                    System.Diagnostics.Debug.Write(machineManager.IpAddress);
                    var results = machineManager.RunScript(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/ServerProvisioning_v2.ps1"));**/
                    var credentials = new PSCredential("newtdemo", new NetworkCredential("", "Newt!dEmo123").SecurePassword);
                    var connectionInfo = new WSManConnectionInfo("https", "newtdemo.eastus.cloudapp.azure.com", 5986, "/wsman", "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", credentials);
                    connectionInfo.SkipCACheck = true;
                    connectionInfo.SkipCNCheck = true;
                    //var runspace = RunspaceFactory.CreateRunspace(connectionInfo);
                    Runspace runspace = null;
                    var remoteScriptFullpath = "C:\\Script\\Working\\ServerProvisioning_v2.ps1";
                    //var remoteScriptFullpath2 = "C:\\Script\\Working\\ServerProvisioning_v3.ps1";
                    var remoteScriptFullpath3 = "C:\\Script\\Working\\ServerProvisioning_v4.ps1";
                    bool isExecutionSuccessful = true;
                    string output = string.Empty;
                    string errors = string.Empty;
                    Collection<PSObject> results = new Collection<PSObject>();
                    Pipeline pipeline = null;
                    StringBuilder stringBuilder = new StringBuilder();
                    try
                    {
                        InvokeAsyncPSCall(connectionInfo, formBean, remoteScriptFullpath, remoteScriptFullpath3);
                        //InvokeAsyncPSCallMock(connectionInfo, formBean, "C:\\Script\\Working\\test.ps1");
                        //System.Threading.Thread.Sleep(660000);

                        //InvokeSCOMCall(connectionInfo, formBean, remoteScriptFullpath3);
                        //string ipAddress = getIPAddress(connectionInfo, formBean, remoteScriptFullpath3); 
                        //InvokeSCOMCallMockNew(ipAddress, formBean);
                        //InvokeSCOMCallMock(ipAddress,formBean);
                        //InvokeSymantecCallMock(ipAddress, formBean);

                        /**runspace = RunspaceFactory.CreateRunspace(connectionInbfo);
                        System.Diagnostics.Debug.WriteLine("runspace:" + runspace);
                        runspace.Open();
                        pipeline = runspace.CreatePipeline();
                        System.Diagnostics.Debug.WriteLine("pipeline:" + pipeline);
                        var cmd = new Command(remoteScriptFullpath);
                        cmd.Parameters.Add("res_grp_name", "EYNewtRes");
                        cmd.Parameters.Add("location", formBean.Location);
                        cmd.Parameters.Add("vnet_name", formBean.Vnet_name);
                        cmd.Parameters.Add("subnet_name", formBean.Subnet_name);
                        cmd.Parameters.Add("sec_grp_name", formBean.Sec_grp_name);
                        cmd.Parameters.Add("nic_name", formBean.Nic_name);
                        cmd.Parameters.Add("vm_name", formBean.Vm_name);
                        cmd.Parameters.Add("disk_name", formBean.Diskname);
                        cmd.Parameters.Add("publicipname", formBean.Publicipname);
                        if (formBean.Os.Equals("win2012R2datacenter"))
                        {
                            formBean.Os = "eynewtimage2";
                        }
                        cmd.Parameters.Add("os", formBean.Os);
                        cmd.Parameters.Add("username", formBean.Username);
                        cmd.Parameters.Add("password", formBean.Password);
                        pipeline.Commands.Add(cmd);
                        results = pipeline.Invoke();
                        foreach (PSObject obj in results)
                        {
                            stringBuilder.AppendLine(obj.ToString());
                        }
                        if (pipeline.Error.Count > 0)
                        {
                            System.Diagnostics.Debug.WriteLine(pipeline.Error.ToString());
                            //errors += String.Join(Environment.NewLine, pipeline.Error.ReadToEnd().Select(er => er.ToString()));
                            isExecutionSuccessful = false;
                        }**/
                    }
                    catch (Exception exp)
                    {
                        System.Diagnostics.Debug.WriteLine("Error caught");
                        errors += "Error occurred in PowerShell script: " + exp.Message + Environment.NewLine + exp.Source + Environment.NewLine + exp.StackTrace + Environment.NewLine + exp.InnerException;
                        isExecutionSuccessful = false;
                        throw new Exception(errors);
                    }
                    finally
                    {
                        System.Diagnostics.Debug.WriteLine("StringBuilder:"+ stringBuilder);
                        //pipeline.Stop();
                        //runspace.Close();
                    }
                    
                    if (isExecutionSuccessful)
                    {
                        System.Diagnostics.Debug.WriteLine("Remote Execution Successful.");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Remote Execution unsuccessful.");
                        System.Diagnostics.Debug.WriteLine(errors);
                    }
                    Response.Redirect("successPage.html");
                    /**HttpContext ctx = HttpContext.Current;
                    ctx.Items["outputMsg"] = output;**/
                }
                //Server.Transfer("ResultsPage.aspx", false);
            }
        }

        public void InvokeAsyncPSCall(WSManConnectionInfo connectionInfo, portalBean formBean, string remoteScriptFullpath,string remoteScriptFullpath3)
        {
            Task.Run(() =>
            {
                using (var runspace = RunspaceFactory.CreateRunspace(connectionInfo))
                {
                    System.Diagnostics.Debug.WriteLine("runspace:" + runspace);
                    runspace.Open();
                    using (Pipeline pipeline = runspace.CreatePipeline())
                    {
                        System.Diagnostics.Debug.WriteLine("Calling InvokeAsyncPSCall:");
                        var cmd = new Command(remoteScriptFullpath);
                        cmd.Parameters.Add("res_grp_name", "EYNewtRes");
                        cmd.Parameters.Add("location", formBean.Location);
                        cmd.Parameters.Add("vnet_name", formBean.Vnet_name);
                        cmd.Parameters.Add("subnet_name", formBean.Subnet_name);
                        cmd.Parameters.Add("sec_grp_name", formBean.Sec_grp_name);
                        cmd.Parameters.Add("nic_name", formBean.Nic_name);
                        cmd.Parameters.Add("vm_name", formBean.Vm_name);
                        cmd.Parameters.Add("disk_name", formBean.Diskname);
                        cmd.Parameters.Add("publicipname", formBean.Publicipname);
                        if (formBean.Os.Equals("win2012R2datacenter"))
                        {
                            formBean.Os = "image1";
                        }
                        cmd.Parameters.Add("os", formBean.Os);
                        cmd.Parameters.Add("username", formBean.Username);
                        cmd.Parameters.Add("password", formBean.Password);
                        pipeline.Commands.Add(cmd);
                        pipeline.Invoke();
                        System.Diagnostics.Debug.WriteLine("AFter PS Call");
                    }
                }
                InvokeSCOMCall(connectionInfo, formBean, remoteScriptFullpath3);
            });
        }

        public void InvokeAsyncPSCallMock(WSManConnectionInfo connectionInfo, portalBean formBean, string remoteScriptFullPath)
        {
            try
            {
                using (var runspace = RunspaceFactory.CreateRunspace(connectionInfo))
                {
                    System.Diagnostics.Debug.WriteLine("runspace for SCOM call:" + runspace);
                    runspace.Open();
                    using (Pipeline pipeline = runspace.CreatePipeline())
                    {
                        System.Diagnostics.Debug.WriteLine("Calling SCOMAgent call");
                        var cmd = new Command(remoteScriptFullPath);
                        cmd.Parameters.Add("res_grp_name", "EYNewtRes");
                        cmd.Parameters.Add("vm_name", formBean.Vm_name);
                        pipeline.Commands.Add(cmd);
                        pipeline.Invoke();
                    }
                }
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine(exp);
                System.Diagnostics.Debug.WriteLine("Message exception:" + exp);
                throw new Exception("Exception found:" + exp.Message + Environment.NewLine + exp.Source + Environment.NewLine + exp.StackTrace + Environment.NewLine + exp.InnerException);
            }

        }

        public void InvokeSCOMCall(WSManConnectionInfo connectionInfo, portalBean formBean,string remoteScriptFullpath3)
        {
            var credentials = new PSCredential(formBean.Username, new NetworkCredential("", formBean.Password).SecurePassword);
            //Task.Run(() =>
            //{
                //System.Threading.Thread.Sleep(600000);
                string ipAddress = getIPAddress(connectionInfo, formBean, remoteScriptFullpath3);
                var connectionInfonew = new WSManConnectionInfo("http", ipAddress, 5985, "/wsman", "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", credentials);
                //connectionInfonew.SkipCACheck = true;
                //connectionInfonew.SkipCNCheck = true;
                using (var runspace = RunspaceFactory.CreateRunspace(connectionInfonew))
                {
                    System.Diagnostics.Debug.WriteLine("runspace for SCOM call:" + runspace);
                    runspace.Open();
                    using (Pipeline pipeline = runspace.CreatePipeline())
                    {
                        System.Diagnostics.Debug.WriteLine("Calling SCOMAgent call");
                        var cmd = new Command("F:\\SCOM\\InstallSCOMAgent.bat");
                        pipeline.Commands.Add(cmd);
                        pipeline.Invoke();
                        System.Diagnostics.Debug.WriteLine("After SCOMAgent call");
                }
                }
            InvokeSymantecCall(ipAddress, formBean, remoteScriptFullpath3);
                //SendAsyncEmailMessage(formBean, ipAddress);
            //});
        }

        public void InvokeSCOMCallMock(string ipAddress, portalBean formBean)
        {
            var credentials = new PSCredential(formBean.Username, new NetworkCredential("", formBean.Password).SecurePassword);
            WSManConnectionInfo connectionInfo = new WSManConnectionInfo("http", ipAddress, 5985, "/wsman", "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", credentials);
            //connectionInfo.SkipCACheck = true; connectionInfo.EnableNetworkAccess = true;
            //connectionInfo.SkipCNCheck = true;
            try {
            using (var runspace = RunspaceFactory.CreateRunspace(connectionInfo))
            {
                System.Diagnostics.Debug.WriteLine("runspace for SCOM call:" + runspace);
                runspace.Open();
                using (Pipeline pipeline = runspace.CreatePipeline())
                {
                    System.Diagnostics.Debug.WriteLine("Calling SCOMAgent call");
                    var cmd = new Command("F:\\SCOM\\InstallSCOMAgent.bat");
                    pipeline.Commands.Add(cmd);
                    pipeline.Invoke();
                }
            }
            }catch(Exception exp)
            {
                System.Diagnostics.Debug.WriteLine(exp);
                System.Diagnostics.Debug.WriteLine("Message exception:"+exp);
                throw new Exception("Exception found:"+exp.Message + Environment.NewLine + exp.Source + Environment.NewLine + exp.StackTrace + Environment.NewLine + exp.InnerException);
            }

        }

        public void InvokeSCOMCallMockNew(string ipAddress, portalBean formBean)
        {
            var credentials = new PSCredential(formBean.Username, new NetworkCredential("", formBean.Password).SecurePassword);
            WSManConnectionInfo connectionInfo = new WSManConnectionInfo("https", "newtdemo.eastus.cloudapp.azure.com", 5986, "/wsman", "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", credentials);
            connectionInfo.SkipCACheck = true;
            connectionInfo.SkipCNCheck = true;
            try
            {
                using (var runspace = RunspaceFactory.CreateRunspace(connectionInfo))
                {
                    System.Diagnostics.Debug.WriteLine("runspace for SCOM call new:" + runspace);
                    runspace.Open();
                    using (Pipeline pipeline = runspace.CreatePipeline())
                    {
                        System.Diagnostics.Debug.WriteLine("Calling SCOMAgent call new");
                        var cmd = new Command("C:\\Script\\Working\\ServerProvisioning_v3.ps1");
                        cmd.Parameters.Add("ipAddress", ipAddress);
                        cmd.Parameters.Add("username", formBean.Username);
                        cmd.Parameters.Add("password", formBean.Password);
                        pipeline.Commands.Add(cmd);
                        pipeline.Invoke();
                    }
                }
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine(exp);
                System.Diagnostics.Debug.WriteLine("Message exception:" + exp);
                throw new Exception("Exception found:" + exp.Message + Environment.NewLine + exp.Source + Environment.NewLine + exp.StackTrace + Environment.NewLine + exp.InnerException);
            }

        }

        public void InvokeSymantecCall(string ipAddress, portalBean formBean,string remoteScriptFullpath3)
        {
            var credentials = new PSCredential(formBean.Username, new NetworkCredential("", formBean.Password).SecurePassword);
            WSManConnectionInfo connectionInfo = new WSManConnectionInfo("http", ipAddress, 5985, "/wsman", "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", credentials);
            //connectionInfo.SkipCACheck = true;
            //connectionInfo.SkipCNCheck = true;
            //Task.Run(() =>
            //{

                using (var runspace = RunspaceFactory.CreateRunspace(connectionInfo))
                {
                    System.Diagnostics.Debug.WriteLine("runspace for Symantec call:" + runspace);
                    runspace.Open();
                    using (Pipeline pipeline = runspace.CreatePipeline())
                    {
                        System.Diagnostics.Debug.WriteLine("Calling InvokeSymantecCall:");
                        var cmd = new Command("F:\\Script\\symantecinstallation.bat");
                        pipeline.Commands.Add(cmd);
                        pipeline.Invoke();
                        System.Diagnostics.Debug.WriteLine("After InvokeSymantecCall:");
                }
                }
            SendAsyncEmailMessage(formBean, ipAddress);
            //});
        }

        public void InvokeSymantecCallMock(string ipAddress, portalBean formBean)
        {
            var credentials = new PSCredential(formBean.Username, new NetworkCredential("", formBean.Password).SecurePassword);
            WSManConnectionInfo connectionInfo = new WSManConnectionInfo("http", ipAddress, 5985, "/wsman", "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", credentials);
            //connectionInfo.SkipCACheck = true;
            //connectionInfo.SkipCNCheck = true;
            try
            {
                using (var runspace = RunspaceFactory.CreateRunspace(connectionInfo))
                {
                    System.Diagnostics.Debug.WriteLine("runspace for Symantec call:" + runspace);
                    runspace.Open();
                    using (Pipeline pipeline = runspace.CreatePipeline())
                    {
                        System.Diagnostics.Debug.WriteLine("Calling InvokeSymantecCallMock:");
                        var cmd = new Command("F:\\Script\\symantecinstallation.bat");
                        pipeline.Commands.Add(cmd);
                        pipeline.Invoke();
                        System.Diagnostics.Debug.WriteLine("Called InvokeSymantecCallMock:");
                    }
                }
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine(exp);
                System.Diagnostics.Debug.WriteLine("Message exception:" + exp);
                throw new Exception("Exception found:" + exp.Message + Environment.NewLine + exp.Source + Environment.NewLine + exp.StackTrace + Environment.NewLine + exp.InnerException);
            }
        }

        public string getIPAddress(WSManConnectionInfo connectionInfo, portalBean formBean, string remoteScriptFullpath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var credentials = new PSCredential(formBean.Username, new NetworkCredential("", formBean.Password).SecurePassword);
            Collection<PSObject> results = null;
            using (var runspace = RunspaceFactory.CreateRunspace(connectionInfo))
            {
                System.Diagnostics.Debug.WriteLine("runspace for ipAddress:" + runspace);
                runspace.Open();
                using (Pipeline pipeline = runspace.CreatePipeline())
                {
                    System.Diagnostics.Debug.WriteLine("Calling getIPAddress");
                    var cmd = new Command(remoteScriptFullpath);
                    cmd.Parameters.Add("res_grp_name", "EYNewtRes");
                    cmd.Parameters.Add("publicipname", formBean.Publicipname);
                    pipeline.Commands.Add(cmd);
                    results = pipeline.Invoke();
                    foreach (PSObject obj in results)
                    {
                        stringBuilder.Append(obj.ToString());
                    }
                }
            }
            string ipAddress = stringBuilder.ToString();
            System.Diagnostics.Debug.WriteLine("ipAddress:" + ipAddress);
            return ipAddress;
        }

        private void SendAsyncEmailMessage(portalBean formBean,string ipAddress)
        {
            //Task.Run(() =>
            //{
                //System.Threading.Thread.Sleep(180000);
                StringBuilder emailMsg = new StringBuilder();
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                //int portNumber = 25;
                bool enableSSL = true;
                string emailTo = "newtglobaldemo@gmail.com";
                string emailFrom = "newtglobaldemo2@gmail.com";
                string passwordFrom = "newtglobal21234!";
                string subject = "Successful provisioning of VM";
                string imageSrc = "<img src='https://cdn3.iconfinder.com/data/icons/flat-actions-icons-9/792/Tick_Mark_Dark-128.png' width=25>";
                string body = "<div style='border: medium solid grey; display: inline-block;font-family: arial,sans-serif; font-size: 14px;'>";
                body += "<h3 style='background-color: red; margin-top:0px;text-align:center'>VM Provisioning Automation Tool Results</h3>";
                body += "<br />";
                body += "Dear User";
                body += "<br />";
                body += "<p>";
                body += "We are happy to inform you that your requested VM is successfully provisioned with the desired softwares and in " + formBean.Location + "</p>";
                body += "<br />";
                body += "<h4>Please check the summary of the resolution below:</h4>";
                body += " <br />";
                body += " <br />";
                body += "<table border=1 style='text-align:center;margin-left:4cm'><tr><th>S.No.</th><th>Artifact</th><th>Artifact Id</th><th>Status</th></tr>";
                body += "<tr><td>1</th><th>Virtual Network</td><td>" + formBean.Vnet_name + "</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>2</th><th>Resource Group</td><td>EYNewtRes</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>3</th><th>Subnet Group</td><td>" + formBean.Subnet_name + "</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>4</th><th>Public IP Name</td><td>"+formBean.Publicipname+"</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>4</th><th>Public IP</td><td>"+ipAddress+"</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>5</th><th>Network Security Group</td><td>" + formBean.Sec_grp_name + "</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>6</th><th>NSG Rule</td><td>rdpRule</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>7</th><th>NSG Rule</td><td>WinRM HTTP Rule</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>8</th><th>Virtual NIC</td><td>" + formBean.Nic_name + "</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>9</th><th>Virtual Name</td><td>" + formBean.Vm_name + "</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>10</th><th>Monitoring Agent</td><td>SCOM</td><td>" + imageSrc + "</td></tr>";
                body += "<tr><td>10</th><th>Endpoint Protection</td><td>Symantec</td><td>" + imageSrc + "</td></tr></table>";
                body += "<br />";
                body += "<br />";
                body += "Thanks,";
                body += "<br />";
                body += "<b>Build and Provisioning Team</b>";
                body += "</div>";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, passwordFrom);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
                System.Diagnostics.Debug.WriteLine("Mail is Invoked");
            //});
        }
    }
}