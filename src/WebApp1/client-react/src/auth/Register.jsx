import {useState} from "react";
import Button from "../form-controls/Button.jsx";
import Input from "../form-controls/Input.jsx";

export default function Register() {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    
    function handleSubmit(e) {
        console.log(password, confirmPassword, email);
        e.preventDefault();
        console.log('handleSubmit');    
    }
    
    return (
         <>
            <div className="m-5">
                <div className="p-5" style={{ width: '400px' }}>
                    <form>
                        <div className="flex justify-between">
                            <label htmlFor="email">Email</label>
                            <Input value={email} 
                                   id="email" 
                                   name="email" 
                                   type="email" 
                                   className=" border  border-green-950" 
                                   placeholder="Enter Email" 
                                   onChange={(e) => setEmail(e.target.value)}>
                            </Input>
                        </div>
                        <div className="flex justify-between">
                            <label htmlFor="password">Password</label>
                            <Input value={password} 
                                   id="password" 
                                   name="password" 
                                   type="password" 
                                   className=" border  border-green-950" 
                                   placeholder="Enter Password" 
                                   onChange={(e) => setPassword(e.target.value)}/>
                            
                        </div>
                        <div className="flex justify-between">
                            <label htmlFor="confirmPassword">Confirm Password</label>
                            <Input value={confirmPassword} 
                                   id="confirmPassword" 
                                   name="confirmPassword" 
                                   type="password" 
                                   className=" border  border-green-950" 
                                   placeholder="Confirm Password"
                            onChange={(e) => setConfirmPassword(e.target.value)}/>
                        </div>
                        <div className="flex justify-end">
                            <Button label="Register"
                                    onClick={handleSubmit}
                            ></Button>
                        </div>
                    </form>
                </div>
            </div>
        </>
    )
}