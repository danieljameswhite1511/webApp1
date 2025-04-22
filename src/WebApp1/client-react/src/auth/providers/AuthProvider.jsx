import {createContext, useContext, useEffect, useState} from 'react';
import authService from "../services/auth.service.js";


export const AuthContext = createContext(null);
export const AuthProvider = ({children}) => {
    
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    useEffect(() => {
        async function isUserAuthenticated() {
            await authService.isAuthenticated()
            .then(response => {
                console.log(response);
                setIsAuthenticated(response.data);
            }).catch(error => {
                    console.log('not authenticated', error)
                console.log(error);
            });
        }   
        
        isUserAuthenticated();
    });
    
    const value = {
        isAuthenticated
    }
    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    )
}