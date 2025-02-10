import React from "react"

class AddTask extends React.Component{
    taskAdd = {}
    constructor(props){
        super(props)
        this.state = {
            task_text: "",
            isDone: false   
        }
    }
    render(){
        return (
            <form ref={(el) => this.myForm = el}>
                <textarea placeholder="Описание" onChange={(e) => this.setState({task_text: e.target.value})}/>
                <button type="button" onClick={() => {
                    this.myForm.reset();
                    this.taskAdd = {
                        task: this.state.task_text,
                    }
                    // if(this.props.task)
                    //     this.taskAdd.id = this.props.task.id
                    this.props.onAdd(this.taskAdd)
                }}>
                Добавить
                </button>
            </form>
        )
    }
}

export default AddTask