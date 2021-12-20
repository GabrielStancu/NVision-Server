using Core.Models;

namespace Infrastructure.Filtering.Specifications
{
    public class SubjectNameSpecification : ISpecification<Subject>
    {
        private readonly string _name;
        public SubjectNameSpecification(string name)
        {
            _name = name;
        } 

        public bool IsSatisfied(Subject t)
        {
            return t.FullName.ToUpper().Contains(_name.ToUpper());
        }
    }
}
