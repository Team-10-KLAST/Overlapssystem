```mermaid 
classDiagram
    direction TB

    %% ========================================
    %% DOMAIN
    %% ========================================

    class Resident {
        +int ResidentId
        +string Initials
        +string ShoppingDay
        +PaymentMethod PaymentMethod
    }

    class ResidentSchema {
        +int ResidentSchemaId
        +string Status
        +string RelativeContact
        +TrafficLight TrafficLight
    }

    class Department {
        +int DepartmentId
        +string DepartmentName
    }

    class Shift {
        +int Id
        +DateTime Date
        +ShiftType Type
        +int DepartmentId
    }

    class Employee {
        +int EmployeeId
        +string FirstName
        +string LastName
    }

    class MedicationTime {
        +int MedicationTimeId
        +TimeOnly PlannedTime
    }

    class MedicationRegistration {
        +bool Given
        +DateTime GivenTime
    }

    class PN {
        +int PNId
        +TimeOnly GivenTime
    }

    class Phone {
        +int Id
        +string Number
        +bool IsTested
    }

    class DepartmentNote {
        +string Content
        +DateTime LastUpdated
    }

    class DepartmentTask {
        +int Id
        +string TaskName
    }

    class ShiftTaskAssignment {
        +int Id
        +string AssignedEmployee
    }

    class ActivityLogEntry {
        +string ActionType
        +string Description
        +DateTime Timestamp
    }

    %% ========================================
    %% ENUMS
    %% ========================================

    class TrafficLight {
        <<enumeration>>
        Green
        Yellow
        Red
    }

    class PaymentMethod {
        <<enumeration>>
        OwnCard
        PCard
        Cash
    }

    class ShiftType {
        <<enumeration>>
        Dagvagt
        Aftenvagt
        Nattevagt
    }

    %% ========================================
    %% DOMAIN RELATIONS
    %% ========================================

    Resident --> Department
    Resident --> MedicationTime
    Resident --> PaymentMethod

    ResidentSchema --> Resident
    ResidentSchema --> Shift
    ResidentSchema --> Employee : Primary
    ResidentSchema --> Employee : Secondary
    ResidentSchema --> MedicationRegistration
    ResidentSchema --> TrafficLight

    MedicationRegistration --> MedicationTime
    MedicationRegistration --> ResidentSchema

    MedicationTime --> Resident

    PN --> ResidentSchema

    Phone --> Employee

    DepartmentTask --> Department

    ShiftTaskAssignment --> Shift
    ShiftTaskAssignment --> DepartmentTask

    ActivityLogEntry --> Employee
    ActivityLogEntry --> ResidentSchema

    Shift --> ShiftType

    %% ========================================
    %% APPLICATION — INTERFACES
    %% ========================================

    class IResidentSchemaService {
        <<interface>>
        +GetByShift()
        +InitializeSchemaForShift()
    }

    class IResidentStatusService {
        <<interface>>
        +UpdateStatus()
    }

    class ITrafficLightService {
        <<interface>>
        +UpdateTrafficLight()
    }

    class IMedicationService {
        <<interface>>
        +MarkAsGiven()
    }

    class IPNService {
        <<interface>>
        +RegisterPN()
        +GetPNsBySchema()
        +DeletePN()
    }

    class IShiftService {
        <<interface>>
        +CreateShift()
        +GetAllShifts()
        +GetShiftById()
    }

    class IEmployeeService {
        <<interface>>
        +GetAllEmployees()
        +CreateEmployee()
    }

    class IAssignEmployeeService {
        <<interface>>
        +AssignEmployee()
    }

    class IPhoneService {
        <<interface>>
        +GetAllPhones()
        +UpdatePhoneAssignment()
    }

    class IDepartmentNoteService {
        <<interface>>
        +GetNote()
        +SaveNote()
    }

    class IDepartmentTaskService {
        <<interface>>
        +GetTasksByDepartment()
        +SaveAssignment()
        +CreateTask()
        +DeleteTask()
    }

    class IActivityLogService {
        <<interface>>
        +AddLogEntry()
        +GetByEmployee()
    }

    class IRelativeContactService {
        <<interface>>
        +Update()
    }

    class ICreateShiftUseCase {
        <<interface>>
        +Execute()
    }

    %% ========================================
    %% APPLICATION — SERVICES
    %% ========================================

    class ResidentSchemaService {
        +GetByShift()
        +InitializeSchemaForShift()
    }

    class ResidentStatusService {
        +UpdateStatus()
    }

    class TrafficLightService {
        +UpdateTrafficLight()
    }

    class MedicationService {
        +MarkAsGiven()
    }

    class PNService {
        +RegisterPN()
        +GetPNsBySchema()
        +DeletePN()
    }

    class ShiftService {
        +CreateShift()
        +GetAllShifts()
        +GetShiftById()
    }

    class EmployeeService {
        +GetAllEmployees()
        +CreateEmployee()
    }

    class AssignEmployeeService {
        +AssignEmployee()
    }

    class PhoneService {
        +GetAllPhones()
        +UpdatePhoneAssignment()
    }

    class DepartmentNoteService {
        +GetNote()
        +SaveNote()
    }

    class DepartmentTaskService {
        +GetTasksByDepartment()
        +SaveAssignment()
        +CreateTask()
        +DeleteTask()
    }

    class ActivityLogService {
        +AddLogEntry()
        +GetByEmployee()
    }

    class RelativeContactService {
        +Update()
    }

    class CreateShiftUseCase {
        +Execute()
    }

    %% ========================================
    %% SERVICE REALIZATIONS
    %% ========================================

    ResidentSchemaService ..|> IResidentSchemaService
    ResidentStatusService ..|> IResidentStatusService
    TrafficLightService ..|> ITrafficLightService
    MedicationService ..|> IMedicationService
    PNService ..|> IPNService
    ShiftService ..|> IShiftService
    EmployeeService ..|> IEmployeeService
    AssignEmployeeService ..|> IAssignEmployeeService
    PhoneService ..|> IPhoneService
    DepartmentNoteService ..|> IDepartmentNoteService
    DepartmentTaskService ..|> IDepartmentTaskService
    ActivityLogService ..|> IActivityLogService
    RelativeContactService ..|> IRelativeContactService

    CreateShiftUseCase ..|> ICreateShiftUseCase

    %% ========================================
    %% SERVICE DEPENDENCIES
    %% ========================================

    ResidentStatusService --> IActivityLogService
    TrafficLightService --> IActivityLogService
    MedicationService --> IActivityLogService
    PNService --> IActivityLogService

    CreateShiftUseCase --> IShiftService
    CreateShiftUseCase --> IResidentSchemaService

    %% ========================================
    %% INFRASTRUCTURE
    %% ========================================

    class AppDbContext

    %% ========================================
    %% DTOs
    %% ========================================

    class ResidentSchemaDto
    class MedicationTimeDto
    class UpdateStatusDto
    class UpdateTrafficLightDto
    class MarkMedicationGivenDto
    class PNDto
    class ShiftDto
    class CreateShiftDto
    class EmployeeDto
    class AssignEmployeeDto
    class PhoneDto
    class DepartmentNoteDto
    class DepartmentTaskDto
    class RelativeContactDto
    class ActivityLogEntryDto

    ResidentSchemaDto --> MedicationTimeDto

    %% ========================================
    %% API — CONTROLLERS
    %% ========================================

    class ResidentSchemaController {
        +GetByShift()
        +AssignEmployee()
    }

    class ResidentStatusController {
        +UpdateStatus()
    }

    class TrafficLightController {
        +UpdateTrafficLight()
    }

    class MedicationController {
        +MarkGiven()
    }

    class PNController {
        +Register()
        +GetBySchema()
        +Delete()
    }

    class ShiftController {
        +CreateShift()
        +GetShiftById()
        +GetAllShifts()
    }

    class EmployeesController {
        +GetAll()
    }

    class PhonesController {
        +GetAll()
        +Update()
    }

    class DepartmentNoteController {
        +Get()
        +Put()
    }

    class DepartmentTaskController {
        +GetTasks()
        +SaveAssignment()
        +CreateTask()
        +DeleteTask()
    }

    class ActivityLogController {
        +GetByEmployee()
    }

    class RelativeContactController {
        +Update()
    }

    %% ========================================
    %% CONTROLLER → SERVICE RELATIONS
    %% ========================================

    ResidentSchemaController --> IResidentSchemaService
    ResidentSchemaController --> IAssignEmployeeService

    ResidentStatusController --> IResidentStatusService
    TrafficLightController --> ITrafficLightService
    MedicationController --> IMedicationService
    PNController --> IPNService

    ShiftController --> IShiftService
    ShiftController --> ICreateShiftUseCase

    EmployeesController --> IEmployeeService
    PhonesController --> IPhoneService

    DepartmentNoteController --> IDepartmentNoteService
    DepartmentTaskController --> IDepartmentTaskService

    ActivityLogController --> IActivityLogService

    RelativeContactController --> IRelativeContactService

    %% ========================================
    %% CONTROLLER → DTO RELATIONS
    %% ========================================

    ResidentStatusController --> UpdateStatusDto
    TrafficLightController --> UpdateTrafficLightDto
    MedicationController --> MarkMedicationGivenDto

    PNController --> PNDto

    ShiftController --> ShiftDto
    ShiftController --> CreateShiftDto

    PhonesController --> PhoneDto

    DepartmentNoteController --> DepartmentNoteDto

    DepartmentTaskController --> DepartmentTaskDto

    RelativeContactController --> RelativeContactDto

    ActivityLogController --> ActivityLogEntryDto

    ResidentSchemaController --> ResidentSchemaDto
    ResidentSchemaController --> AssignEmployeeDto

    %% ========================================
    %% API — MAPPERS
    %% ========================================

    class ResidentSchemaMapper {
        +ToDto()
    }

    class ShiftMapper {
        +ToDto()
    }

    class EmployeeMapper {
        +ToDto()
    }

    class PNMapper {
        +ToDto()
    }

    class PhoneMapper {
        +ToDto()
    }

    class DepartmentNoteMapper {
        +ToDto()
        +ToEntity()
    }

    class DepartmentTaskMapper {
        +ToDto()
    }

    class ActivityLogMapper {
        +ToDto()
        +ToDtoList()
    }

    %% ========================================
    %% MAPPER RELATIONS
    %% ========================================

    ResidentSchemaMapper --> ResidentSchema
    ResidentSchemaMapper --> ResidentSchemaDto

    ShiftMapper --> Shift
    ShiftMapper --> ShiftDto

    EmployeeMapper --> Employee
    EmployeeMapper --> EmployeeDto

    PNMapper --> PN
    PNMapper --> PNDto

    PhoneMapper --> Phone
    PhoneMapper --> PhoneDto

    DepartmentNoteMapper --> DepartmentNote
    DepartmentNoteMapper --> DepartmentNoteDto

    DepartmentTaskMapper --> DepartmentTask
    DepartmentTaskMapper --> DepartmentTaskDto

    ActivityLogMapper --> ActivityLogEntry
    ActivityLogMapper --> ActivityLogEntryDto

    %%========================================
    %% BLAZOR CLIENT — PAGES
    %% ========================================

    class ChooseShiftPage {
        +HandleSubmit()
    }

    class OverlapsSystemPage {
        +OnInitializedAsync()
    }

    class ActivityLogPage {
        +LoadLog()
    }

    %% ========================================
    %% BLAZOR CLIENT — COMPONENTS
    %% ========================================

    class ResidentCard
    class TrafficLightComponent
    class StatusField
    class FixedMedication
    class PNMedication
    class EmployeeAssignment
    class RelativeContactComponent
    class PhoneList
    class ShiftTaskSection
    class NoteSection

    %% Component hierarchy
    OverlapsSystemPage --> ResidentCard
    OverlapsSystemPage --> PhoneList
    OverlapsSystemPage --> ShiftTaskSection
    OverlapsSystemPage --> NoteSection

    ResidentCard --> TrafficLightComponent
    ResidentCard --> StatusField
    ResidentCard --> FixedMedication
    ResidentCard --> PNMedication
    ResidentCard --> EmployeeAssignment
    ResidentCard --> RelativeContactComponent

    %% ========================================
    %% BLAZOR CLIENT — DATASERVICES
    %% ========================================

    class IResidentSchemaDataService {
        <<interface>>
        +GetByShift()
    }

    class IResidentStatusDataService {
        <<interface>>
        +UpdateStatus()
    }

    class ITrafficLightDataService {
        <<interface>>
        +UpdateTrafficLight()
    }

    class IMedicationDataService {
        <<interface>>
        +MarkAsGiven()
    }

    class IPNDataService {
        <<interface>>
        +GetPNsBySchema()
        +RegisterPN()
        +DeletePN()
    }

    class ISaveShiftService {
        <<interface>>
        +SaveShift()
    }

    class IShiftService_Blazor {
        <<interface>>
        +GetById()
    }

    class IEmployeeDataService {
        <<interface>>
        +GetEmployees()
    }

    class IPhoneDataService {
        <<interface>>
        +GetPhones()
        +UpdatePhone()
    }

    class IDepartmentNoteDataService {
        <<interface>>
        +GetNoteByDepartmentId()
        +UpdateNote()
    }

    class IDepartmentTaskDataService {
        <<interface>>
        +GetTasks()
        +SaveAssignment()
        +CreateTask()
        +DeleteTask()
    }

    class IEmployeeAssignmentDataService {
        <<interface>>
        +AssignEmployee()
    }

    class IRelativeContactDataService {
        <<interface>>
        +Update()
    }

    class IActivityLogDataService {
        <<interface>>
        +GetByEmployee()
    }

    %% ========================================
    %% PAGE → DATASERVICE RELATIONS
    %% ========================================

    ChooseShiftPage --> ISaveShiftService

    ActivityLogPage --> IActivityLogDataService
    ActivityLogPage --> IEmployeeDataService

    OverlapsSystemPage --> IResidentSchemaDataService
    OverlapsSystemPage --> IShiftService_Blazor
    OverlapsSystemPage --> IEmployeeDataService
    OverlapsSystemPage --> IPhoneDataService
    OverlapsSystemPage --> IDepartmentNoteDataService
    OverlapsSystemPage --> IDepartmentTaskDataService

    %% ========================================
    %% DATASERVICE → API FLOW
    %% ========================================

    IResidentSchemaDataService --> ResidentSchemaController
    IResidentStatusDataService --> ResidentStatusController
    ITrafficLightDataService --> TrafficLightController
    IMedicationDataService --> MedicationController
    IPNDataService --> PNController

    ISaveShiftService --> ShiftController
    IShiftService_Blazor --> ShiftController

    IEmployeeDataService --> EmployeesController
    IPhoneDataService --> PhonesController

    IDepartmentNoteDataService --> DepartmentNoteController
    IDepartmentTaskDataService --> DepartmentTaskController

    IRelativeContactDataService --> RelativeContactController

    IActivityLogDataService --> ActivityLogController

    IEmployeeAssignmentDataService --> ResidentSchemaController
    ```