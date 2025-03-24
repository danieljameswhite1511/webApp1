import "./Button.scss"
export default function ({label, onClick}) {
    return (
        <>
            <div className="button-component">
                <button type="button" onClick={onClick}>
                    <span>
                       {label}
                    </span>
                </button>
            </div>
         </>
    )
}