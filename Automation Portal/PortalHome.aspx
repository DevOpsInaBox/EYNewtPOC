<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PortalHome.aspx.cs" Inherits="Automation_Portal.PortalHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <title>Infrastructure Automation</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script> 
    <script>
        $(document).ready(function () {

            $(document).on("click", "#clrBtn", function () {
                $("#portalForm input[type=text], select").each(function () {
                    var input = $(this);
                    if (input.is('select')) {
                        input.prop('selectedIndex', 0);
                        return true;
                    }
                    input.val("");
                });
            });

            $(document).on("click", "#submitBtn", function () {
                $("#portalForm input[type=text], select").each(function () {
                    var input = $(this).val();
                    if (input == undefined || input == null || input.length == 0) {
                        return false;
                    }
                });
                var password = $("#password").val();
                var confpassword = $("#confpassword").val();
                if (password != confpassword) {
                    $("#message").text("Error! Password and Confirm Password are not matching");
                    return false;
                }
            });

        });
    </script>
    <style>
        .header-top {
            background-color: #fff;
            border: 1px solid #28327f;
        }

    </style>
</head>
<body style="background-color: #fbfbfb;">
    <div class="header-top">
        <div class="container">
            <div class="col-md-3"> <img src="ey.jpg" style="width: 275px; height: 100px;"/></div>
            <div class="col-md-6">
                <!--            <img src="" style="width: 100%; height: 400px;" />-->
                <h1 style="text-align: center;margin-top: 6%;">Infrastructure Provisioning Tool</h1>
            </div>
            <div class="col-md-3"> <img src="http://newtglobal.com/wp-content/uploads/2016/03/newtglobal_logo.png"/></div>
        </div>
    </div>
    <div class="container">
        <div id="loginbox" style="margin-top: 50px; text-align: center">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">Enter New VM Details for Infrastructure Automation in Azure</div>
                    <div class="clearfix"></div>
                </div>
                <div style="" class="panel-body">
                    <div style="display: none" id="login-alert" class="alert alert-danger col-sm-12"></div>
                    <form class="form-horizontal" role="form" id="portalForm" runat="server">

                        <div class="col-md-8 col-md-offset-2">
                            <div class="form-group">
                                <div id="message" style="text-align:center"></div>
                                <div class="clearfix"></div>
                                <br/>								
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Virtual Machine Name:</label>
                                    <div class="col-sm-7">
                                        <input type="text" id="vm_name" runat="server" class="form-control" placeholder="" />
                                    </div>
                                </div>
								<div class="form-group">
                                    <label class="control-label col-sm-4">UserName:</label>
                                    <div class="col-sm-7">
                                        <input type="text" id="username" runat="server" class="form-control" placeholder="" />
                                    </div>
                                </div>
								<div class="form-group">
                                    <label class="control-label col-sm-4">Password:</label>
                                    <div class="col-sm-7">
                                        <input type="password" id="password" runat="server" class="form-control" placeholder="" />
                                    </div>
                                </div>
								<div class="form-group">
                                    <label class="control-label col-sm-4">Confirm Password:</label>
                                    <div class="col-sm-7">
                                        <input type="password" id="confpassword" runat="server" class="form-control" placeholder="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Location:</label>
                                    <div class="col-sm-7">
                                        <select id="location" runat="server" class="form-control">
                                            <option value="">Please select Location</option>
                                            <option value="East US">East US</option>
                                            <option value="Central US">Central US</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Virtual Network Name:</label>
                                    <div class="col-sm-7">
                                        <input type="text" id="vnet_name" runat="server" class="form-control" placeholder="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Subnet Name:</label>
                                    <div class="col-sm-7">
                                        <input type="text" id="subnet_name" runat="server" class="form-control" placeholder="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Network Security Group Name:</label>
                                    <div class="col-sm-7">
                                        <input type="text" id="sec_grp_name" runat="server" class="form-control" placeholder="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Network Interface Card Name:</label>
                                    <div class="col-sm-7">
                                        <input type="text" id="nic_name" runat="server" class="form-control" placeholder="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Public IP Name:</label>
                                    <div class="col-sm-7">
                                        <input type="text" id="publicipname" runat="server" class="form-control" placeholder="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">New Disk Label:</label>
                                    <div class="col-sm-7">
                                        <input type="text" id="diskname" runat="server" class="form-control" placeholder="" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-4">Operating System:</label>
                                    <div class="col-sm-7">
                                        <select id="os" runat="server" class="form-control">
                                            <option value="">Please select OS</option>
                                            <option value="win2012R2datacenter">Windows Server 2012 R2 Datacenter</option>
                                            <option value="win2012Datacenter">Windows Server 2012 Datacenter</option>
                                            <option value="win2008R2SP1">Windows Server 2008 R2 SP1</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div style="text-align: center">
                                <span><button type="submit" id="submitBtn" class="btn btn-success" style="margin-right: 8px;">Submit Request</button></span>
                                <span><input type="button" id="clrBtn" value="Clear" class="btn btn-danger" runat="server" /></span>
                            </div>
                            <br />
                            <br />

                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>

</html>