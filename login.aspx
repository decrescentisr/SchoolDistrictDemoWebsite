<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SchoolDistWeb.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css/w3.css"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="css/Site.css" />
    <link rel="stylesheet" href="css/style.css" />
    
    <link rel="stylesheet" href="css/w3.css" />
    <title></title>
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
    <div class="hero-image">
        <img src="images/registrationhero.jpg" class="responsive" height="600"/>
        <div class="hero-text">
            <h1 style="font-size:50px; text-align: center; color: blue;">Login Portal</h1>
        </div>
    </div>
    <br />
    <br />
    <form id="form1" runat="server">

        <asp:Label ID="lblUsername" runat="server" Text="User Name: "></asp:Label>
        &nbsp;<asp:TextBox ID="txtUsername" runat="server"></asp:TextBox><br />

        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        <br />

        <asp:CheckBox ID="chkRemember" runat="server" Text="Remember Me Next Time" />
        <br />
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
       
        <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-primary" OnClick="btnLogin_Click"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRegister" runat="server" PostBackUrl="~/Register.aspx" Text="Register" class ="btn btn-primary"/>
        <div>
        </div>
    </form>
    <footer>
        <div class="jumbotron-footer">
            <hr style="color:grey"/>
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
