import React from "react"
// import Task from "./Task"

class Tasks extends React.Component {
    // constructor(props){
    //     super(props)
    //     this.state = {

    //     }
    // }
    render(){
        if (this.state.tasks.length > 0)
            return (<div>
                {this.state.tasks.map((el) => (
                    <Task onEdit={this.props.onEdit} task={el}/>
                ))
                }
            </div>)
        else
            return (<div className="task">
                <h3>Пусто</h3>
            </div>)
    }
}

export default Tasks