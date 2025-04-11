using System;
public class Task
{
    internal Task Prev, Next;

    // C의 경우, 여기서 디폴트 new 연산자와 delete 연산자를 무효화함(힙 영역 메모리 확보 사용하지 않기 위함)
    // 캐릭터 클래스는 태스크 클래스로부터 파생시킨다. 또한, 이 파생 클래스에서 새로운 new와 delete를 오버로딩 한다.
    // 혹시 오버로드를 깜박 잊고 구현 안 한 상태에서, new나 delete를 불러도 태스크 클래스의 new와 delete가 호출되고
    // 'private 맴버는 호출할 수 없음'이라는 컴파일러 에러가 발생한다. 그래서 파생 클래스에서 new/delete의 오버로딩을
    // 깜빡한 것을 알려주게 된다.

    public void Create(TaskList list)
    {
    	Prev=list.ActiveTask.Prev;
    	Next=list.ActiveTask;
    	Prev.Next=Next.Prev=this;
        Console.WriteLine("테스크 생성");
    }
    public void Destroy()
    {
        Prev.Next = Next;
        Next.Prev = Prev;
        Console.WriteLine("테스크 삭제");
    }

    //void* operator new(size_t n) {}
    //C# 에서는 new 연산자 오버로딩 불가능
    //따라서 private 생성자로 new키워드 막음
    public Task(TaskList list)
    {
        Prev=list.ActiveTask.Prev;
    	Next=list.ActiveTask;
    	Prev.Next=Next.Prev=this;
        Console.WriteLine("테스크 생성");
    }
    //void operator delete(void* p) {}
    //C#은 객체 제거시 GC에서 자동처리 소멸자 불필요
    // public virtual ~Task()
    // {
    //     Prev->Next=Next;
    //     Next->Prev=Prev;
    //     std::cout << "테스크 삭제" << std::endl;
    // }
}
