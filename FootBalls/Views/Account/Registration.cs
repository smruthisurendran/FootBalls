<!DOCTYPE html>
<!-- saved from url=(0042)http://localhost:7865/Account/Registration -->
<html><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width">
    <title>Registration</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <!-- Font Icon -->
    <link rel="stylesheet" href="./Registration_files/material-design-iconic-font.min.css">

    <!-- Main css -->
    <link rel="stylesheet" href="./Registration_files/style.css">

</head>
<body>
    <div>
        


<meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta http-equiv="X-UA-Compatible" content="ie=edge">
<!-- Font Icon -->
<link rel="stylesheet" href="./Registration_files/material-design-iconic-font.min.css">

<!-- Main css -->
<link rel="stylesheet" href="./Registration_files/style.css">


<div class="signup-form">

<form action="http://localhost:7865/Account/Registration" class="appointment-form" method="post">        <div class="main">


            <div class="container">
                
                    <div class="form-input">

                       
                        <input class=" text-box single-line" id="Name" name="Name" placeholder="Your Name" type="text" value="">
                        <span class="field-validation-valid " data-valmsg-for="Name" data-valmsg-replace="true"></span>
                    </div>

                    <div class="form-input">
                       
                        <input class=" text-box single-line" id="Password" name="Password" placeholder="Password" type="text" value="">
                        <span class="field-validation-valid " data-valmsg-for="Password" data-valmsg-replace="true"></span>
                    </div>

                    <div class="form-input">

                        
                        <input class=" text-box single-line" data-val="true" data-val-email="Please enter a valid email" id="EmailId" name="EmailId" placeholder="Email-ID" type="email" value="">
                        <span class="field-validation-valid " data-valmsg-for="EmailId" data-valmsg-replace="true"></span>
                    </div>

                    <div class="form-input">


                        <input class=" text-box single-line" id="MobileNumber" name="MobileNumber" placeholder="Mobile Number" type="text" value="">
                        <span class="field-validation-valid " data-valmsg-for="MobileNumber" data-valmsg-replace="true"></span>
                    </div>

                    <div class="form-input">

                       
                        <select class="form-control" data-val="true" data-val-number="The field Category must be a number." data-val-required="The Category field is required." id="Category" name="Category"><option value="">Category</option>
<option value="1">Private</option>
<option value="2">Private Sector</option>
<option value="3">Govt. Institution</option>
</select>

                        
                        <span class="field-validation-valid " data-valmsg-for="Category" data-valmsg-replace="true"></span>
                    </div>
                    <div class="form-submit">
                        <input type="submit" name="submit" id="submit" class="submit" value="Submit">
                    </div>
                    
            </div>
        </div></form>
   
</div>


    </div>


</body></html>