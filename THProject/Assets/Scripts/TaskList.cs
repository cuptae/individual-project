using System;
using System.Diagnostics;
public class TaskList
{
    internal Task[] Buffer;
    internal Task ActiveTask, FreeTask;
    int TaskSize, TaskCount;

    Task GetTask(int i)
    {
        return Buffer[i];
    }



    public TaskList(int size, int count)
    {
        TaskSize = size;
        TaskCount = count;

        Buffer = new Task[TaskSize *(TaskCount + 2)];
        ActiveTask = GetTask(0);
        ActiveTask.Prev = ActiveTask.Next = ActiveTask;

        FreeTask = GetTask(1);
        for(int i = 1; i< TaskCount+2; i++)
        {
            GetTask(i).Prev = null;
            GetTask(i).Next=GetTask(i+1);
        }
        GetTask(TaskCount +1).Next =FreeTask;
    }

    ~TaskList()
    {
        Buffer = null;
    }

    public object New(int size)
    {
        Console.WriteLine($"{size} TaskList New");
        if(size<=TaskSize)
        {
            Console.WriteLine("New의 로 할당한 사이즈가 TaskSize보다 작습니다");
        }	// 에러 검출용 코드
        if(FreeTask.Next == FreeTask) return null;

        Task task=FreeTask.Next;
        FreeTask.Next=task.Next;
        return task;
    }
    public void Delete(object p)
    {
        Console.WriteLine($"{p} TaskList Delete");
        Task task=(Task)p;
        //assert(task->Prev!=NULL);
        
        task.Prev=null;
        task.Next=FreeTask.Next;
        FreeTask.Next=task;
    }
}