using System.Threading.Tasks;

public class TaskIter
{
    private TaskList List;
    Task Task;

    public TaskIter(TaskList list)
    {
        List = list;
        Task = list.ActiveTask;
    }

    public bool HasNext()
    {
        return Task.Next != List.ActiveTask;
    }

    public Task Next()
    {
        return Task = Task.Next;
    }

    public void Remove()
    {
        this.Task = this.Task.Prev;
	    this.Task.Next = null;
    }
}

// class CTaskIter {

// private:
// 	CTaskList* List;
// 	CTask* Task;

// public:
// 	CTaskIter(CTaskList* list);
// 	bool HasNext();
// 	CTask* Next();
// 	void Remove();
// };

// CTaskIter::CTaskIter(CTaskList* list)
// :	List(list), Task(list->ActiveTask)
// {}

// bool CTaskIter::HasNext() {
// 	return Task->Next!=List->ActiveTask;
// }

// CTask* CTaskIter::Next() {
// 	return Task=Task->Next;
// }

// void CTaskIter::Remove() {
// 	Task=Task->Prev;
// 	delete Task->Next;
// }