import axios from "axios";
class UserService {
 
  constructor() {
   this.apiUrl = `${import.meta.env.VITE_API_URL}/users`;
  }
  
  async getUsers(){
      console.log(this.apiUrl)
   await axios.get(this.apiUrl, {
       withCredentials: true,
   });
  }
  
 
 }
 
 const userService = new UserService();
 export default userService;