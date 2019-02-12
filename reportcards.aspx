<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportcards.aspx.cs" Inherits="SchoolDistWeb.reportcards" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Report Cards</title>

    <link rel="stylesheet" href="css/w3.css"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/Site.css" />
    <link rel="stylesheet" href="css/style.css" />
</head>
<body>
    <!-- Navbar (sit on top) -->
    <div class="w3-top">
        <div class="w3-bar" id="myNavbar">
            <a class="w3-bar-item w3-button w3-hover-black w3-hide-medium w3-hide-large w3-right" href="javascript:void(0);" onclick="toggleFunction()" title="Toggle Navigation Menu">
                <i class="fa fa-bars"></i>
            </a>
            <a href="javascript:void(0);" onclick="toggleFunction()" title="School District Demo Website" class="navbar-brand"><p class="w3-bar-item"> <b>School District</b></p> </a>
            <a href="contact.html" class="w3-bar-item w3-button w3-hide-small"><i class="fa fa-envelope"></i> CONTACT</a>
            <a href="social.html" class="w3-bar-item w3-button w3-hide-small"><i class="fa fa-th"></i> SOCIAL</a>
            <a href="about.html" class="w3-bar-item w3-button w3-hide-small"><i class="fa fa-user"></i> ABOUT</a>
            <a href="students.html" class="w3-bar-item w3-button w3-hide-small"><i class="fa fa-briefcase"></i>&nbsp;STUDENTS</a>
            <a href="login.aspx" class="w3-bar-item w3-button w3-hide-small"><i class="fa fa-briefcase"></i>&nbsp;STAFF</a>
            <a href="index.html" class="w3-bar-item w3-button"><i class="fa fa-home"></i> &nbsp; HOME</a>

        </div>

        <!-- Navbar on small screens -->
        <div id="navDemo" class="w3-bar-block w3-white w3-hide w3-hide-large w3-hide-medium">
            <a href="students.html" class="w3-bar-item w3-button" onclick="toggleFunction()"><i class="fa fa-briefcase"></i>&nbsp;STUDENTS</a>
            <a href="login.aspx" class="w3-bar-item w3-button"><i class="fa fa-rss"></i> &nbsp;STAFF</a>
            <a href="about.html" class="w3-bar-item w3-button" onclick="toggleFunction()"><i class="fa fa-user"></i> &nbsp; ABOUT</a>
            <a href="social.html" class="w3-bar-item w3-button" onclick="toggleFunction()"><i class="fa fa-th"></i>&nbsp;SOCIAL</a>
            <a href="contact.html" class="w3-bar-item w3-button" onclick="toggleFunction()"><i class="fa fa-envelope"></i> &nbsp;CONTACT</a>

        </div>
    </div>

    <div class="hero-image" >
        <img src="images/reportcard.jpg" class="responsive" width="1600" height="800" style="opacity: .80"/>
        <div class="hero-text">
            <h1 style="font-size:50px; color: black;">Upload Report Cards</h1>            
        </div>
    </div>

    <br />

      <center><img src="images/reportcards.png" /></center>
     <h2 style="text-align: center; font-size: 30px;">Please upload your student's report cards for parent viewing</h2>
    <br />

    <form id="form1" runat="server">
        <div>
           
            &nbsp;&nbsp; <asp:Label ID="lblFirstName" runat="server" Text="Student First Name:"></asp:Label>
            <br />
            &nbsp;&nbsp; <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ErrorMessage="This Field is Required" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            &nbsp;&nbsp; <asp:Label ID="lblLastName" runat="server" Text="Student's Last Name:"></asp:Label>
            <br />
            &nbsp;&nbsp; <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" ErrorMessage="This Field is Required" ForeColor="Red"></asp:RequiredFieldValidator>

            <br />

            <br />
             &nbsp;&nbsp; <asp:FileUpload ID="upReport" runat="server" />
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="upReport" ErrorMessage="This Field is Required" ForeColor="Red"></asp:RequiredFieldValidator>

            <br />
            <br />
            <p>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False" Text="You Successfully Uploaded the Report Card!...This page will redirect to the portal in 10 seconds..."></asp:Label>
    </p>
            <p>
                <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
    </p>
        <br />
        </div>
    </form>
     <footer>
        <div class="jumbotron-footer">
            <hr style="color:grey">
            <div class="row">
                <div class="col-sm-2">
                    <p><img src="images/schooldistlogosm_edited.jpg" style="width: 150px; height: 90px;" /> </p><br />
                    <p style="font-size:12px;">School District is a full service school district website focusing on providing information and solutions to parents, educators and students.</p>
                </div>
                <div class="col-sm-3">
                    <h2 style="font-size:25px">Fun Legal Stuff</h2>

                    <a href="terms.html" style="text-decoration:none;">Terms & Conditions</a><br />
                    <a href="privacy.html" style="text-decoration:none;">Privacy Policy</a><br />
                </div>
                <div class="col-sm-3">
                    <h2 style="font-size: 25px">About School District</h2>
                    <a href="about.html" style="text-decoration:none;">About Us</a><br />

                </div>

                <div class="col-sm-3">
                    <h2 style="font-size: 25px">Home</h2>
                    <a href="index.html" style="text-decoration:none;">Home Page</a><br />

                </div>
            </div>

        </div>
        <div class="jumbotron-footer">
            <p style="text-align:center">&copy; 2019 &nbsp;&nbsp;School District All Rights Reserved. &nbsp;&nbsp; <a href="sitemap.html" style="text-decoration:none;">Site Map</a></p>
        </div>

    </footer>
        <script>
            // Change style of navbar on scroll
        window.onscroll = function () { myFunction() };
        function myFunction() {
            var navbar = document.getElementById("myNavbar");
            if (document.body.scrollTop > 100 || document.documentElement.scrollTop > 100) {
                navbar.className = "w3-bar" + " w3-card" + " w3-animate-top" + " w3-white";
            } else {
                navbar.className = navbar.className.replace(" w3-card w3-animate-top w3-white", "");
            }
        }

        // Used to toggle the menu on small screens when clicking on the menu button
        function toggleFunction() {
            var x = document.getElementById("navDemo");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
            } else {
                x.className = x.className.replace(" w3-show", "");
            }
        }
        </script>
</body>
</html>
