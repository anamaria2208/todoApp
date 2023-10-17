function validateEmail (email: string){
    debugger
    const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailPattern.test(email);
}

function validatePasswordLength(password : string){
    const passwordPattern = /^.{6,}$/;
    return passwordPattern.test(password)
}



export const emailValidation =  validateEmail;
export const passwordLengthValidation =  validatePasswordLength;