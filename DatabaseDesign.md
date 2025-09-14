# Database Design

This document describes the tables, columns, primary keys, foreign keys, and relationships for the Library Management System.

---

## Tables

### Address
- **Id**: GUID PK
- **Street**: string
- **City**: string
- **State**: string (nullable)
- **LocationMapUrl**: string (nullable)
- **Type**: enum `AddressType`
- **PersonId**: GUID (nullable, FK to Person)

**Relationships**:
- Optional 1–1 with Person

---

### Person
- **Id**: GUID PK
- **FirstName**: string
- **SecondName**: string (nullable)
- **ThirdName**: string (nullable)
- **LastName**: string
- **DateOfBirth**: DateTime
- **Gender**: enum `GenderType`
- **ProfileImageUrl**: string (nullable)
- **AddressId**: GUID (nullable, FK to Address)

**Relationships**:
- 1–M Contacts
- 1–1 optional Nationality
- 1–M Registrations

---

### User
- **Id**: GUID PK
- **RegistrationId**: GUID FK to Registration

**Relationships**:
- 1–1 from Registration
- 1–M Subscriptions
- 1–M PaymentLogs
- 1–M Reviews (optional)

---

### Registration
- **Id**: GUID PK
- **PersonId**: GUID FK to Person
- **Status**: enum `RegistrationStatus`
- Navigation: User 1–1

---

### BookTitle
- **Id**: int PK
- **Title**: string
- **ISBN**: string
- **PublisherId**: GUID FK
- **CategoryId**: int FK
- **LanguageId**: int FK

**Relationships**:
- 1–M BookCopies
- M–M BookAuthors via BookAuthor
- 1–M Reviews

---

### BookCopy
- **Id**: GUID PK
- **BookTitleId**: int FK
- **PublisherId**: GUID FK
- **ShelfId**: GUID FK (nullable)
- **BranchId**: GUID FK (nullable)
- **Barcode**: string (nullable)
- **CopyStatus**: enum `BookCopyStatus`
- **PhysicalState**: enum `BookPhysicalState`
- **RemovedDate**: DateTime? (nullable)

**Relationships**:
- 1–M Borrowings
- 1–M Reservations

---

### BookAuthor
- **BookTitleId**: int FK
- **AuthorId**: int FK

**Relationships**:
- M–M join table between BookTitle and Author

---

### Author
- **Id**: int PK
- **Name**: string
- **AuthorDetailId**: int FK (nullable)

**Relationships**:
- 1–1 optional AuthorDetail
- M–M BookTitles via BookAuthor

---

### AuthorDetail
- **Id**: int PK
- **CountryId**: GUID FK (nullable)
- **Alias**: string (nullable)
- **Awards**: string (nullable)
- **BiographyExtended**: string (nullable)

**Relationships**:
- 1–1 Author

---

### Branch
- **Id**: GUID PK
- **Name**: string
- **Type**: enum `BranchType`
- **AddressId**: GUID FK
- **ManagedByStaffId**: GUID FK (nullable)

**Relationships**:
- 1–M Events
- 1–M Staffs
- 1–M Shelves
- 1–M BookCopies

---

### Shelf
- **Id**: GUID PK
- **Code**: string
- **BranchId**: GUID FK

**Relationships**:
- 1–M BookCopies

---

### Borrowing
- **Id**: GUID PK
- **UserId**: GUID FK
- **BookCopyId**: GUID FK
- **ReservationId**: GUID FK (nullable)
- **PricePerDay**: decimal
- **NumberOfLateDays**: byte
- **Status**: enum `BorrowingStatus`
- **BorrowedAt**: DateTime
- **DueAt**: DateTime? (nullable)
- **ReturnedAt**: DateTime? (nullable)

**Relationships**:
- 1–M PaymentLogs

---

### Reservation
- **Id**: GUID PK
- **UserId**: GUID FK
- **BookCopyId**: GUID FK
- **BorrowingId**: GUID FK (nullable)
- **ReservationDate**: DateTime
- **ExpiryDate**: DateTime
- **Status**: enum `ReservationStatus`

---

### Publisher
- **Id**: GUID PK
- **Name**: string
- **AddressId**: GUID FK (nullable)

**Relationships**:
- 1–M BookTitles
- 1–M BookCopies

---

### Category
- **Id**: int PK
- **Name**: string
- **Description**: string (nullable)

**Relationships**:
- 1–M BookTitles

---

### Language
- **Id**: int PK
- **Name**: string

**Relationships**:
- 1–M BookTitles

---

### Membership
- **Id**: GUID PK
- **MembershipType**: enum
- **IsActive**: bool
- **Description**: string (nullable)

**Relationships**:
- 1–M Subscriptions
- 1–M PaymentLogs
- 1–M Users

---

### Subscription
- **Id**: GUID PK
- **UserId**: GUID FK
- **MembershipId**: GUID FK
- **BranchId**: GUID FK (nullable)
- **StartDate**: DateTime
- **EndDate**: DateTime
- **IsExtended**: bool
- **ExtendedUntil**: DateTime? (nullable)
- **Status**: enum `SubscriptionStatus`

**Relationships**:
- 1–M PaymentLogs

---

### PaymentLog
- **Id**: GUID PK
- **UserId**: GUID FK
- **BorrowingId**: GUID FK (nullable)
- **MembershipId**: GUID FK (nullable)
- **Amount**: decimal
- **PaymentDate**: DateTime
- **PaymentType**: enum
- **PaymentMethod**: enum
- **PaymentStatus**: enum
- **Currency**: enum
- **ParentPaymentId**: GUID FK (nullable)
- **Notes**: string (nullable)

**Relationships**:
- 1–M ChildPayments (self-reference)

---

### Contact
- **Id**: GUID PK
- **PersonId**: GUID FK
- **Type**: enum `ContactType`
- **Value**: string
- **IsPrimary**: bool

**Relationships**:
- 1–M Notifications

---

### Notification
- **Id**: GUID PK
- **ContactId**: GUID FK
- **Message**: string
- **Status**: enum `MessageStatus`
- **IsRead**: bool
- **DeliveredAt**: DateTime? (nullable)
- **ReadAt**: DateTime? (nullable)

---

### Staff
- **Id**: GUID PK
- **PersonId**: GUID FK
- **BranchId**: GUID FK
- **EmployeeNumber**: string
- **HireDate**: DateTime
- **Position**: string

**Relationships**:
- M–M ManagedBranches
- 1–M StaffAttendance
- 1–M Branches Managed (optional)

---

### StaffAttendance
- **Id**: GUID PK
- **StaffId**: GUID FK
- **AttendanceTime**: DateTime
- **IsPresent**: bool

---

### Event
- **Id**: GUID PK
- **Name**: string
- **Description**: string
- **StartTime**: DateTime
- **EndTime**: DateTime
- **BranchId**: GUID FK (nullable)

**Relationships**:
- M–M EventActiveStaff

---

### Nationality
- **Id**: GUID PK
- **PersonId**: GUID FK
- **CountryId**: GUID FK
- **ResidencyId**: GUID FK (nullable)
- **NationalIdNumber**: string? (nullable)
- **PassportNumber**: string? (nullable)

---

### Residency
- **Id**: GUID PK
- **PersonId**: GUID FK
- **CountryId**: GUID FK
- **ResidencyNumber**: string
- **ValidUntil**: DateTime? (nullable)

---

### Review
- **Id**: GUID PK
- **UserId**: GUID FK (nullable)
- **BookTitleId**: int FK (nullable)
- **Comment**: string
- **ReviewRate**: enum `ReviewRate`

---

### Country
- **Id**: GUID PK
- **Name**: string
- **ISOCode**: string

**Relationships**:
- 1–M Nationalities
- 1–M Residencies

---

## Relationships Summary

- **1–1**: Person ↔ Address, Registration ↔ User, Author ↔ AuthorDetail
- **1–M**: Person → Contacts, User → Subscriptions, Branch → BookCopies, Branch → Staffs
- **M–M**: BookTitle ↔ Author (via BookAuthor), Event ↔ Staff (EventActiveStaff), Staff ↔ Branch (ManagedBranches)
- **Self-reference**: PaymentLog → ParentPayment → ChildPayments

## Entity Relationship Diagram

```mermaid
erDiagram
    Person ||--o{ Registration : has
    Person ||--o{ Contact : has
    Person ||--|| Address : lives_at
    Person ||--o{ Nationality : has
    Person ||--o{ Residency : has

    Registration ||--|| User : links
    User ||--o{ Subscription : subscribes
    User ||--o{ Borrowing : borrows
    User ||--o{ PaymentLog : pays
    User ||--o{ Review : writes

    BookTitle ||--o{ BookCopy : has
    BookTitle ||--o{ Review : reviewed
    BookTitle }o--o{ Author : written_by
    BookTitle }o--|| Publisher : published_by
    BookTitle }o--|| Category : categorized_as
    BookTitle }o--|| Language : in

    BookCopy ||--o{ Borrowing : borrowed
    BookCopy ||--o{ Reservation : reserved
    BookCopy }o--|| Shelf : stored_on
    BookCopy }o--|| Branch : belongs_to

    Reservation ||--|| Borrowing : may_convert

    Membership ||--o{ Subscription : includes
    Membership ||--o{ PaymentLog : has

    Subscription ||--o{ PaymentLog : generates

    PaymentLog ||--o{ PaymentLog : child_payments

    Branch ||--o{ Shelf : contains
    Branch ||--o{ BookCopy : owns
    Branch ||--o{ Staff : employs
    Branch ||--o{ Event : hosts

    Staff ||--o{ StaffAttendance : tracks
    Staff }o--o{ Branch : manages
    Staff }o--o{ Event : participates

    Author ||--|| AuthorDetail : details
    AuthorDetail }o--|| Country : from

    Nationality }o--|| Country : belongs_to
    Residency }o--|| Country : in

    Notification }o--|| Contact : sent_to

