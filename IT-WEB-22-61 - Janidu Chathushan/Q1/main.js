// function validateEmail() {
//     var email = document.getElementById('email');
//     var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
//     //var emailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  
//     if (emailRegex.test(String(email).toLowerCase())) {
//       alert('Valid email address!');
//     } else {
//       alert('Invalid email address!');
//     }
//   }
  
function validateEmail(type) {
    var email = document.getElementById(type).value;
   
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  
    if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(email)) {
      alert('Valid email address!');
    } else {
      alert('Invalid email address!');
    }
  }
  