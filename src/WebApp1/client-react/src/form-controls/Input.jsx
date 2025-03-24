import "./Input.scss"
export default function Input ({id, name, label, placeholder, type, value, errors, onChange}) {
    
    return (
        <>
        <div className="input-component">
            <label htmlFor={id}>{label}</label>
            <input type={type} 
                   id={id} 
                   name={name} 
                   value={value} 
                   onChange={onChange} 
                   placeholder={placeholder} />

            { errors &&  <div className="errors">
                
            </div>}
            
        </div>
        </>
    )
}