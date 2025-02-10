import React from "react"
import AddTask from "./AddTask"
// import { FaExchangeAlt } from "react-icons/fa"
// import { MdDelete } from "react-icons/md"

class Task extends React.Components{
    constructor(props){
        super(props)
        this.state = {
            editForm: false
        }
    }
    task = this.props.task
    render(){
        return(
            <div clasName="task">
                {/* <MdDelete className="delete-icon" onClick={() => this.props.onDelete(this.task.id)}/>
                <FaExchangeAlt className="edit-icon" onClick={() => {
                    this.setState({ 
                        editForm: !this.state.editForm
                    })
                }}/> */}
                <p>{this.task.task}</p>
                <b>{this.task.isDone ? 'Выполнено' : 'Не выполнено'}</b>
                {this.state.editForm && <AddTask task={this.task} onAdd={this.props.onEdit}/>}
            </div>
        )
    }
}

export default Task