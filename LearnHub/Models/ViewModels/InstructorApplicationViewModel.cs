// الملف: Models/ViewModels/InstructorApplicationViewModel.cs

using System.Collections.Generic;
using LearnHub.Models;

namespace LearnHub.Models.ViewModels
{
    public class InstructorApplicationViewModel
    {
        // 1. نموذج تقديم الطلب (البيانات التي سيتم حفظها)
        public InstructorApplication Application { get; set; }

        // 2. قائمة المدربين المعتمدين (البيانات التي سيتم عرضها)
        public IEnumerable<InstructorApplication> ApprovedInstructors { get; set; }
    }
}