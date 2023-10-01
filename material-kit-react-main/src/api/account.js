
const User = require('./db'); 

const users = []; 

function addUser(firstName, lastName, email, password, role) {

  const existingUser = users.find((user) => user.email === email);

  if (existingUser) {
    console.log("A user with the same email already exists.");
    return false;
  }

  const newUser = { ...User, firstName, lastName, email, password, role };
  users.push(newUser);
  console.log(users);
  console.log("User added successfully.");
  return true;
}


function login(email, password){
    
    const existingUser = users.find( (user)=> user.email === email && user.password === password);
  
    if(existingUser) {
        console.log("Login successfully");
        return true;
    }

    console.log("Wrong password or email");
    return false;
}


function loginKaz (e, p)
{
  return true;
}
module.exports = { addUser, login, users, loginKaz }; 
