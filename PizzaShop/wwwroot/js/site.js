document.addEventListener('DOMContentLoaded', function () {
    const togglePassword = document.querySelector("#togglePassword");
    const password = document.querySelector("#password");

    // Password toggle functionality
    if (togglePassword && password) {
        togglePassword.addEventListener("click", function () {
            console.log('togglePassword clicked');
            
            
            const type = password.getAttribute("type") === "password" ? "text" : "password";
            
            password.setAttribute("type", type);

            const normalIcon = this.querySelector('.normal-icon');
            const activeIcon = this.querySelector('.active-icon');

            if (normalIcon && activeIcon) {                
                if (normalIcon.style.display !== 'inline-block') {                    
                    normalIcon.style.display = 'inline-block';
                    activeIcon.style.display = 'none';
                } else {
                    normalIcon.style.display = 'none';
                    activeIcon.style.display = 'inline-block';
                }
            }
        });
    }
});

// MAIN WEBSITE JS

// const navLinks = document.querySelectorAll('.c-side');

// navLinks.forEach(link => {
//     link.addEventListener('click', function (event) {
//         navLinks.forEach(navLink => {
//             navLink.classList.remove('active');

//             const normalIcon = navLink.querySelector('.normal-icon');
//             const activeIcon = navLink.querySelector('.active-icon');

//             if (normalIcon && activeIcon) {
//                 normalIcon.style.display = 'inline-block';
//                 activeIcon.style.display = 'none';
//             }
//         });

//         this.classList.add('active');

//         const normalIcon = this.querySelector('.normal-icon');
//         const activeIcon = this.querySelector('.active-icon');

//         if (normalIcon && activeIcon) {
//             normalIcon.style.display = 'none';
//             activeIcon.style.display = 'inline-block';
//         }
//     });
// });

function toggleMenuForBigScreen(){
    const offcanvas = document.querySelector('.offcanvas');

    if (window.innerWidth > 991) {
        offcanvas.classList.add('show');
    }
}


window.addEventListener('resize', function(event){
    const offcanvas = document.querySelector('.offcanvas');

    if (window.innerWidth > 991) {
        offcanvas.classList.add('show');
    }
    else{
        offcanvas.classList.remove('show');
    }
  });