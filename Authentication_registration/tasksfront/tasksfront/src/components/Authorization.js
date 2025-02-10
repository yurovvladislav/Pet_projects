import React from "react"

class Authorization extends React.Component{
    constructor(props){
        super(props)
        this.state = {
            email: "",
            username: "",
            password: ""
        }
    }
    render(){
        return(
            <form>
                <input placeholder="Почта" onChange={(e) => this.setState({email: e.target.value})}/>
                <input placeholder="Никнейм" onChange={(e) => this.setState({username: e.target.value})}/>
                <input placeholder="Пароль" onChange={(e) => this.setState({password: e.target.value})}/>
                <button type="button" onClick={() => {
                    this.myForm.reset();
                    this.vopros = {
                        email: this.state.email,
                        username: this.state.username,
                        password: this.state.password,
                    }
                }}>

                </button>
            </form>
        )
    }
}

export default Authorization