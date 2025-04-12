import "./Button.scss"
export default function ({label, onClick, disabled}) {
    return (
        <>
            <div className="button-component">
                <button type="button" onClick={onClick} disabled={disabled} className="disabled:bg-gray-600">
                    <span>
                       {disabled ? 'Submitting' : label}
                    </span>
                </button>
            </div>
         </>
    )
}