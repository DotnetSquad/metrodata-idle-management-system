using API.Contracts;
using API.DataTransferObjects.Employees;
using API.Models;
using API.Utilities.Handlers;

namespace API.Services;

public class EmployeeService
{
    private IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IEnumerable<EmployeeDtoGet> Get()
    {
        var employees = _employeeRepository.GetAll().ToList();
        if (!employees.Any()) return Enumerable.Empty<EmployeeDtoGet>();
        List<EmployeeDtoGet> employeeDtoGets = new();

        foreach (var employee in employees)
        {
            employeeDtoGets.Add((EmployeeDtoGet)employee);
        }

        return employeeDtoGets;
    }

    public EmployeeDtoGet? Get(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null) return null!;

        return (EmployeeDtoGet)employee;
    }

    public EmployeeDtoCreate? Create(EmployeeDtoCreate employeeDtoCreate)
    {
        Employee employee = employeeDtoCreate;
        employee.Nik = GenerateHandler.GenerateNik(_employeeRepository.GetLastEmployeeNik());

        var employeeCreated = _employeeRepository.Create(employee);
        if (employeeCreated is null) return null!;

        return (EmployeeDtoCreate)employeeCreated;
    }

    public int Update(EmployeeDtoUpdate employeeDtoUpdate)
    {
        var employee = _employeeRepository.GetByGuid(employeeDtoUpdate.Guid);
        if (employee is null) return -1;

        var employeeUpdated = _employeeRepository.Update(employeeDtoUpdate);
        return !employeeUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null) return -1;

        var employeeDeleted = _employeeRepository.Delete(employee);
        return !employeeDeleted ? 0 : 1;
    }
}
