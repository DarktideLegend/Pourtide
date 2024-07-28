DELETE FROM `weenie` WHERE `class_Id` = 606009;

INSERT INTO `weenie` (`class_Id`, `class_Name`, `type`, `last_Modified`)
VALUES (606009, 'ace606009-thornarmorgem', 38, '2024-06-26 05:32:01') /* Gem */;

INSERT INTO `weenie_properties_int` (`object_Id`, `type`, `value`)
VALUES (606009,   1,       2048) /* ItemType - Gem */
     , (606009,   5,         10) /* EncumbranceVal */
     , (606009,  16,     524296) /* ItemUseable - SourceContainedTargetContained */
     , (606009,  19,          1) /* Value */
     , (606009,  53,        101) /* PlacementPosition - Resting */
     , (606009,  93,       1044) /* PhysicsState - Ethereal, IgnoreCollisions, Gravity */
     , (606009,  94,          6) /* TargetType - Vestements */;

INSERT INTO `weenie_properties_bool` (`object_Id`, `type`, `value`)
VALUES (606009,  11, True ) /* IgnoreCollisions */
     , (606009,  13, True ) /* Ethereal */
     , (606009,  14, True ) /* GravityStatus */
     , (606009,  19, True ) /* Attackable */
     , (606009,  69, False) /* IsSellable */
     , (606009, 60001, True );

INSERT INTO `weenie_properties_string` (`object_Id`, `type`, `value`)
VALUES (606009,   1, 'Thorn Armor Gem') /* Name */
     , (606009,  14, 'Use this gem on any armor piece to apply reflective damage properties to it. Damage taken to armor pieces imbued with this gem will reflect a % of flat damage back to the attacker.') /* Use */;

INSERT INTO `weenie_properties_d_i_d` (`object_Id`, `type`, `value`)
VALUES (606009,   1,   33555677) /* Setup */
     , (606009,   3,  536870932) /* SoundTable */
     , (606009,   8,  100689554) /* Icon */
     , (606009,  22,  872415275) /* PhysicsEffectTable */;

/* Lifestoned Changelog:
{
  "LastModified": "2024-06-24T21:55:46.9309497-07:00",
  "ModifiedBy": "pourman",
  "Changelog": [],
  "UserChangeSummary": "Weenie exported from ACEmulator world database using ACE.Adapter",
  "IsDone": true
}
*/
