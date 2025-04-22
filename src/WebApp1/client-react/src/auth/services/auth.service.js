import axios from "axios"
import {SignInMethod} from "../constants/sign-in-method.constant.js";
 class AuthService {
    
    constructor() {
        this.apiUrl = import.meta.env.VITE_API_URL;
    }
    
   async  register(email, password, confirmPassword) {
      let requestBody ={
          email: email,
          password: password,
          confirmPassword: confirmPassword
      }
      return new Promise(async (resolve, reject) => {
          await axios.post(`${this.apiUrl}/users`, requestBody)
              .then((response) => {
                  resolve(response)
              }).catch((error) => {
                  reject(error.response.data)
                  console.log(error.response.data)
              }).finally(() => {});    
      });
    }
    
    async confirmEmail(userId, token) {
        return new Promise(async (resolve, reject) => {
            await axios.get(`${this.apiUrl}/users/verify`, {
                params: {
                    userId: userId,
                    token: token
                },
            }).then(async (response) => {
                resolve(response)
            }).catch((error) => {
                reject(error)
            });
        });
    }
    
    async login(email, password) {
        return new Promise(async (resolve, reject) => {
            let requestBody = {
                email: email,
                password: password,
                signInMethod: SignInMethod.webUi
            }
            await axios.post(`${this.apiUrl}/users/sign-in`, requestBody, {
                withCredentials: true
            })
                .then((response) => {
                    resolve(response)
                }).catch((error) => {
                    reject(error)
                });
        })
    }
    
    async isAuthenticated() {
        return new Promise(async (resolve, reject) => {
            await axios.get(`${this.apiUrl}/users/is-authenticated`, {
                withCredentials: true
            }).then(async (response) => {
                resolve(response)
            }).catch((error) => {
                reject(error)
            })
        })
     }
}



const authService = new AuthService();
export default authService;
