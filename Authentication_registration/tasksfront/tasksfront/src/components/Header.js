import React from "react"
import Button from "./Button"

class Header extends React.Component{
    render(){
        return (
            <header>
                {this.props.title}
                <Button text="sdf"/>
            </header>
        )
    }
}

export default Header